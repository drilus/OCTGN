/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Linq;
using System.Reflection;
using log4net;
using System.Threading;

using Octgn.Communication;
using Octgn.Communication.Chat;
using Octgn.Communication.Serializers;

namespace Octgn.Online.GameService
{

    public class GameBot : IDisposable
    {
        internal static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Singleton

        internal static GameBot SingletonContext { get; set; }

        private static readonly object GameBotSingletonLocker = new object();

        public static GameBot Instance {
            get {
                if (SingletonContext == null) {
                    lock (GameBotSingletonLocker) {
                        if (SingletonContext == null) {
                            SingletonContext = new GameBot();
                        }
                    }
                }
                return SingletonContext;
            }
        }

        #endregion Singleton

        private readonly Client _chatClient;

        private GameBot()
        {
            _chatClient = new Client(new TcpConnection(AppConfig.Instance.ServerPath), new XmlSerializer());
            _chatClient.InitializeChat();
            _chatClient.RequestReceived += ChatClient_RequestReceived;
        }

        public void Start()
        {
            _chatClient.Connect(AppConfig.Instance.XmppUsername, AppConfig.Instance.XmppPassword);
        }

        private void ChatClient_RequestReceived(object sender, RequestReceivedEventArgs args)
        {
            try {
                var hostGameRequest = HostGameRequest.GetFromPacket(args.Request);
                if (hostGameRequest == null) return;

                Log.InfoFormat("Host game from {0}", args.Request.Origin);
                var endTime = DateTime.Now.AddSeconds(10);
                while (SasUpdater.Instance.IsUpdating) {
                    Thread.Sleep(100);
                    if (endTime > DateTime.Now) throw new Exception("Couldn't host, sas is updating");
                }
                var id = GameManager.Instance.HostGame(hostGameRequest, new Skylabs.Lobby.User(args.Request.Origin)).Result;
                var game = GameManager.Instance.Games.FirstOrDefault(x => x.Id == id);

                HostedGame gameInfo = new HostedGame {
                    GameGuid = game.GameGuid,
                    GameIconUrl = game.GameIconUrl,
                    GameName = game.GameName,
                    GameStatus = game.GameStatus.ToString(),
                    GameVersion = game.GameVersion,
                    HasPassword = game.HasPassword,
                    Id =game.Id,
                    IpAddress = game.IpAddress.ToString(),
                    Name = game.Name,
                    Port = game.Port,
                    Source = game.Source.ToString(),
                    Spectator = game.Spectator,
                    TimeStarted = game.TimeStarted,
                    UserIconUrl = game.UserIconUrl,
                    Username = game.Username
                };

                if (id == Guid.Empty) throw new InvalidOperationException("id == Guid.Empty");

                if (id != Guid.Empty) {
                    args.Response = new Communication.Packets.ResponsePacket(args.Request, gameInfo);
                }
            } catch (Exception ex) {
                Log.Error(nameof(ChatClient_RequestReceived), ex);
            }
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Log.Info(nameof(GameBot) + " Disposed");
        }

        #endregion
    }
}
