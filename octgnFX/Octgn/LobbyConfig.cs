﻿using Skylabs.Lobby;

namespace Octgn
{

    public class LobbyConfig : ILobbyConfig
    {
        #region Singleton

        internal static LobbyConfig SingletonContext { get; set; }

        private static readonly object LobbyConfigSingletonLocker = new object();

        public static LobbyConfig Get()
        {
            lock (LobbyConfigSingletonLocker) return SingletonContext ?? (SingletonContext = new LobbyConfig());
        }

        internal LobbyConfig()
        {
        }

        #endregion Singleton

        public string GameBotUsername { get { return this.GetGameBotUsername(); } }

        public string ChatHost { get { return this.GetChatHost(); } }

        public User GameBotUser { get { return this.GetGameBotUser(); } }

        private User GetGameBotUser()
        {
            return new User(GameBotUsername);
        }

        internal string GetChatHost()
        {
            return AppConfig.ChatServerHost;
        }

        internal string GetGameBotUsername()
        {
            //if (X.Instance.Debug || X.Instance.ReleaseTest)
            //    return "gameserv-test";
            return "gameserv";
        }
    }
}