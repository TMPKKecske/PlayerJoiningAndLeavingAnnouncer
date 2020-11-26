using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;

namespace PlayerJoiningAnnouncer
{
    public class Plugin : Plugin<Config>
    {
        private static readonly Lazy<Plugin> lazyinstance = new Lazy<Plugin>(valueFactory: () => new Plugin());
        public static Plugin Instance => lazyinstance.Value;
        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        private Plugin()
        {

        }

        private Handlers.Player player;

        public override void OnEnabled()
        {
            RegisterEvents();
        }

        public override void OnDisabled()
        {
            UnRegisterEvents();
        }

        public void RegisterEvents()
        {
            player = new Handlers.Player();
            Player.Joined += player.OnJoin;
            Player.Left += player.OnLeft;
        }

        public void UnRegisterEvents()
        {
            Player.Joined -= player.OnJoin;
            Player.Left -= player.OnLeft;
            player = null;
        }
    }
}
