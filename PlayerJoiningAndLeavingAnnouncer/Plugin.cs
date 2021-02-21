using Exiled.API.Features;
using System;
using Player = Exiled.Events.Handlers.Player;

namespace PlayerJoiningAnnouncer
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "TMPKKecske";
        
        public override string Name { get; } = "PlayerJoiningAndLeavingAnnouncer";
       
        public override Version RequiredExiledVersion { get; } = new Version(2, 2, 5); 

        public static Plugin Singleton;

        public Handlers.Player player;
        
        public override void OnEnabled()
        {
            Singleton = this;
            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnRegisterEvents();
            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            player = new Handlers.Player();
            Player.Verified += player.OnVertified;
            Player.Destroying += player.OnDerstroy;            
        }

        public void UnRegisterEvents()
        {
            Player.Verified -= player.OnVertified;
            Player.Destroying -= player.OnDerstroy;            
            player = null;            
        }
    }
}
