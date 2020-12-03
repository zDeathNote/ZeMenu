using GTA;
using System;
using System.Collections.Generic;
using System.Reflection;
using ZeMenu.Commands;
using ZeMenu.Menus.Items;
using ZeMenu.Util;

namespace ZeMenu.Menus
{
    class MenuBuilder
    {
        internal Player P { get; }
        internal MenuBuilder(Player P)
        {
            this.P = P;

            GenerateCommands();
            GenerateMenus();
        }

        private string SpriteLocation { get; } = @".\Scripts\ZeMenu\images\";

        private Command WantedUp, WantedDown, GiveHealth, GiveArmor, RemoveAllItems, GiveWeaponKnife, 
        GiveWeaponBaseballBat, GiveWeaponMolotov, GiveWeaponGrenade,GiveWeaponGlock,GiveWeaponDesertEagle,
        GiveWeaponPumpShotgun,GiveWeaponCombatShotgun, GiveWeaponUzi,GiveWeaponMP5,GiveWeaponAK47,GiveWeaponM4A1,
        GiveWeaponSniperRifle,GiveWeaponCombatSniper, GiveWeaponRPG, GiveMoney, GiveWeaponPoolCue, GiveWeapon9mm, 
        GiveWeaponAssaultShotgun, GiveWeaponTLADGrenadeLauncher, GiveWeaponPipeBomb, GiveWeaponPistol44, GiveWeaponAdvancedShotgun, 
        GiveWeaponExplosiveShotgun, GiveWeaponAdvancedSMG, GiveWeaponAdvancedMG, GiveWeaponAdvancedSniper, GiveWeaponTBOGTGrenadeLauncher, 
        GiveWeaponStickyBomb, GiveWeaponGoldSMG, GiveWeaponSawedOff = null;

        private void GenerateCommands()
        {
            WantedUp = new CommandWantedLevel(P, CommandWantedLevel.WantedLevelType.UP);
            WantedDown = new CommandWantedLevel(P, CommandWantedLevel.WantedLevelType.DOWN);
            GiveHealth = new CommandPlayerGeneral(P, CommandPlayerGeneral.Type.HEAL);
            GiveArmor = new CommandPlayerGeneral(P, CommandPlayerGeneral.Type.ARMOR);
            GiveMoney = new CommandGiveMoney(P);

            RemoveAllItems = new CommandRemoveAllItems(P);
            GiveWeaponKnife = new CommandGiveItem(P, Weapon.Melee_Knife, 1);
            GiveWeaponBaseballBat = new CommandGiveItem(P, Weapon.Melee_BaseballBat, 1);
            GiveWeaponMolotov = new CommandGiveItem(P, Weapon.Thrown_Molotov, 20);
            GiveWeaponGrenade = new CommandGiveItem(P, Weapon.Thrown_Grenade, 20);
            GiveWeaponGlock = new CommandGiveItem(P, Weapon.Handgun_Glock, 100);
            GiveWeaponDesertEagle = new CommandGiveItem(P, Weapon.Handgun_DesertEagle, 100);
            GiveWeaponPumpShotgun = new CommandGiveItem(P, Weapon.Shotgun_Basic, 100);
            GiveWeaponCombatShotgun = new CommandGiveItem(P, Weapon.Shotgun_Baretta, 100);
            GiveWeaponUzi = new CommandGiveItem(P, Weapon.SMG_Uzi, 500);
            GiveWeaponMP5 = new CommandGiveItem(P, Weapon.SMG_MP5, 500);
            GiveWeaponAK47 = new CommandGiveItem(P, Weapon.Rifle_AK47, 500);
            GiveWeaponM4A1 = new CommandGiveItem(P, Weapon.Rifle_M4, 500);
            GiveWeaponSniperRifle = new CommandGiveItem(P, Weapon.SniperRifle_Basic, 25);
            GiveWeaponCombatSniper = new CommandGiveItem(P, Weapon.SniperRifle_M40A1, 25);
            GiveWeaponRPG = new CommandGiveItem(P, Weapon.Heavy_RocketLauncher, 8);

            GiveWeaponPoolCue = new CommandGiveItem(P, Weapon.TLAD_Poolcue, 1);
            GiveWeapon9mm = new CommandGiveItem(P, Weapon.TLAD_Automatic9mm, 100);
            GiveWeaponAssaultShotgun = new CommandGiveItem(P, Weapon.TLAD_AssaultShotgun, 500);
            GiveWeaponSawedOff = new CommandGiveItem(P, Weapon.TLAD_SawedOffShotgun, 100);
            GiveWeaponTLADGrenadeLauncher = new CommandGiveItem(P, Weapon.TLAD_GrenadeLauncher, 20);
            GiveWeaponPipeBomb = new CommandGiveItem(P, Weapon.TLAD_PipeBomb, 20);

            GiveWeaponPistol44 = new CommandGiveItem(P, Weapon.TBOGT_Pistol44, 100);
            GiveWeaponAdvancedShotgun = new CommandGiveItem(P, Weapon.TBOGT_NormalShotgun, 100);
            GiveWeaponExplosiveShotgun = new CommandGiveItem(P, Weapon.TBOGT_ExplosiveShotgun, 100);
            GiveWeaponAdvancedSMG = new CommandGiveItem(P, Weapon.TBOGT_AssaultSMG, 500);
            GiveWeaponAdvancedMG = new CommandGiveItem(P, Weapon.TBOGT_AdvancedMG, 1000);
            GiveWeaponAdvancedSniper = new CommandGiveItem(P, Weapon.TBOGT_AdvancedSniper, 100);
            GiveWeaponTBOGTGrenadeLauncher = new CommandGiveItem(P, Weapon.TBOGT_GrenadeLauncher, 100);
            GiveWeaponStickyBomb = new CommandGiveItem(P, Weapon.TBOGT_StickyBomb, 20);
            GiveWeaponGoldSMG = new CommandGiveItem(P, Weapon.TBOGT_GoldenSMG, 100);
        }

        internal Menu MainMenu;

        private Menu playerMenu;
        private Menu vehicleMenu;
        private Menu weaponsMenu;
        private Menu worldMenu;
        private void GenerateMenus()
        {
            playerMenu = new Menu("Player", new Items.MenuItem[]
            {
                new TextItem("Teleport to Waypoint", new CommandTeleport(P)),
                new ToggleItem("Invincible", "IsInvincible"),
                new TextItem("Give Health", GiveHealth),
                new TextItem("Give Armor", GiveArmor),
                new SpinnerMenuItem("Give <${0}>", 500, 250, int.MinValue, int.MaxValue, Actions:GiveMoney),
                new TextItem("Wanted Up", WantedUp),
                new TextItem("Wanted Down", WantedDown),
                new ToggleItem("Police Ignore You", "WantedIgnored"),
            });


            GenerateVehiclesMenu();

            GenerateWeaponsMenu();

            worldMenu = new Menu("World", new Items.MenuItem[]
            {
                new SpinnerMenuItem("Ped Density <{0}>", 1.0m, 0.1m, 0.0m, 5.0m, new CommandPedDensity(P)),
                new SpinnerMenuItem("Veh Density <{0}>", 1.0m, 0.1m, 0.0m, 5.0m, new CommandVehicleDensity(P)),
                new Menu("Time",
                    new TextItem("+1 Hour", new CommandTime(P, CommandTime.TimeType.HOUR)),
                    new TextItem("+1 Day", new CommandTime(P, CommandTime.TimeType.DAY))
                ),
                new Menu("Weather",
                    new TextItem("Cloudy", new CommandWeather(P, Weather.Cloudy)),
                    new TextItem("Drizzle", new CommandWeather(P, Weather.Drizzle)),
                    new TextItem("ExtraSunny", new CommandWeather(P, Weather.ExtraSunny)),
                    new TextItem("ExtraSunny2", new CommandWeather(P, Weather.ExtraSunny2)),
                    new TextItem("Foggy", new CommandWeather(P, Weather.Foggy)),
                    new TextItem("Raining", new CommandWeather(P, Weather.Raining)),
                    new TextItem("Sunny", new CommandWeather(P, Weather.Sunny)),
                    new TextItem("SunnyAndWindy", new CommandWeather(P, Weather.SunnyAndWindy)),
                    new TextItem("SunnyAndWindy2", new CommandWeather(P, Weather.SunnyAndWindy2)),
                    new TextItem("Thunderstorm", new CommandWeather(P, Weather.ThunderStorm))
                    )
            });

            Menu animationMenu = new Menu("Animations",
                new ToggleItem("Smoking", "IsSmoking")
            );

            MainMenu = new Menu("", new Items.MenuItem[]
                {playerMenu, vehicleMenu,weaponsMenu, worldMenu, animationMenu,
                new TextItem("About", new CommandAbout(P)),
                new ToggleItem("Lock Controls", "LockControls"),
                /*new SpinnerMenuItem("An example <<<{0}>>> spinner", 1, 1, new CommandGiveMoney(P, 0))/*,
                new TextItem("7"),
                new TextItem("8"),
                new TextItem("9"),
                new TextItem("10"),
                new TextItem("11"),
                new TextItem("12"),
                new TextItem("13"),
                new TextItem("14"),
                new TextItem("15"),
                new TextItem("16"),*/
            });
        }

        private readonly Comparer<MenuItem> menuItemComparer = Comparer<MenuItem>.Create((c1, c2) =>
        {
            return c1.DisplayText.CompareTo(c2.DisplayText);
        });

        internal void GenerateVehiclesMenu()
        {
            List<MenuItem> vehItems = new List<MenuItem>();
            foreach (string name in VehicleList.GTAIVModelNames)
            {
                vehItems.Add(new SpriteMenuItem(name.ToUpper(), SpriteLocation + name + ".png", new CommandSpawnVehicle(P, name)));
            }
            switch(Game.CurrentEpisode)
            {
                case GameEpisode.TLAD:
                    foreach (string name in VehicleList.TLADModelNames)
                    {
                        vehItems.Add(new SpriteMenuItem(name.ToUpper(), SpriteLocation + name + ".png", new CommandSpawnVehicle(P, name)));
                    }
                break;
                case GameEpisode.TBOGT:
                    foreach (string name in VehicleList.TBOGTModelNames)
                    {
                        vehItems.Add(new SpriteMenuItem(name.ToUpper(), SpriteLocation + name + ".png", new CommandSpawnVehicle(P, name)));
                    }
                break;
            }

            
            List<MenuItem> vehControl = new List<MenuItem>();
            List<MenuItem> colorControl = new List<MenuItem>
            {
                new TextItem("Current", new CommandVehicleColor(P, CommandVehicleColor.ColorType.Current, ColorIndex.AgateGreen))
            };
            foreach (CommandVehicleColor.ColorType t in Enum.GetValues(typeof(CommandVehicleColor.ColorType)))
            {
                List<MenuItem> l = new List<MenuItem>();
                
                if (t == CommandVehicleColor.ColorType.Current)
                {
                    continue;
                }
                foreach(PropertyInfo c in typeof(ColorIndex).GetProperties(BindingFlags.Public | BindingFlags.Static))
                {
                    l.Add(new SpriteMenuItem(c.Name, (ColorIndex)c.GetValue(null), new CommandVehicleColor(P, t, (ColorIndex)c.GetValue(null))));
                }
                l.Sort(menuItemComparer);
                colorControl.Add(new Menu(t.ToString(), l.ToArray()));
            }

            vehControl.Add(new Menu("General", new MenuItem[]
            {
                new TextItem("Repair", new CommandVehicleGeneral(P, CommandVehicleGeneral.GeneralActions.REPAIR)),
                new ToggleItem("Invincible", "IsVehicleInvincible"),
                new TextItem("Clean", new CommandVehicleGeneral(P, CommandVehicleGeneral.GeneralActions.CLEAN)),
                new TextItem("Dirty", new CommandVehicleGeneral(P, CommandVehicleGeneral.GeneralActions.DIRTY)),
                new ToggleItem("Never Dirty", "VehicleNeverDirty")
            }));
            vehControl.Add(new Menu("Color", colorControl.ToArray()));
            vehControl.Add(new Menu("Doors", new MenuItem[]
            {
                new TextItem("Hood", new CommandVehicleDoor(P, VehicleDoor.Hood)),
                new TextItem("Trunk", new CommandVehicleDoor(P, VehicleDoor.Trunk)),
                new TextItem("FrontL", new CommandVehicleDoor(P, VehicleDoor.LeftFront)),
                new TextItem("FrontR", new CommandVehicleDoor(P, VehicleDoor.RightFront)),
                new TextItem("RearL", new CommandVehicleDoor(P, VehicleDoor.LeftRear)),
                new TextItem("RearR", new CommandVehicleDoor(P, VehicleDoor.RightRear))
            }));

            vehicleMenu = new Menu("Vehicles", 
                new Menu("Spawning", vehItems.ToArray()),
                new Menu("Vehicle Control", vehControl.ToArray())
            );
        }
        
        internal void GenerateWeaponsMenu()
        {
            List<MenuItem> weapPacks = new List<MenuItem>()
            {
                new TextItem("Pack 1", GiveWeaponKnife, GiveWeaponMolotov, GiveWeaponGlock,
                        GiveWeaponPumpShotgun, GiveWeaponUzi, GiveWeaponAK47, GiveWeaponCombatSniper
                ),
                new TextItem("Pack 2", GiveWeaponBaseballBat, GiveWeaponGrenade, GiveWeaponDesertEagle,
                        GiveWeaponCombatShotgun, GiveWeaponMP5, GiveWeaponM4A1, GiveWeaponSniperRifle, GiveWeaponRPG
                )
            };
            List<MenuItem> weapItems = new List<MenuItem>()
            {
                new Menu("IV Weapons",
                    new TextItem("Knife", GiveWeaponKnife),
                    new TextItem("Baseball Bat", GiveWeaponBaseballBat),
                    new TextItem("Molotovs", GiveWeaponMolotov),
                    new TextItem("Grenades", GiveWeaponGrenade),
                    new TextItem("Pistol1", GiveWeaponGlock),
                    new TextItem("Pistol2", GiveWeaponDesertEagle),
                    new TextItem("Shotgun1", GiveWeaponPumpShotgun),
                    new TextItem("Shotgun2", GiveWeaponCombatShotgun),
                    new TextItem("SMG1", GiveWeaponUzi),
                    new TextItem("SMG2", GiveWeaponMP5),
                    new TextItem("Rifle1", GiveWeaponAK47),
                    new TextItem("Rifle2", GiveWeaponM4A1),
                    new TextItem("Sniper1", GiveWeaponCombatSniper),
                    new TextItem("Sniper2", GiveWeaponSniperRifle),
                    new TextItem("RPG", GiveWeaponRPG)
                )
            };
            if (Game.CurrentEpisode == GameEpisode.TLAD)
            {
                weapPacks.Add(
                        new TextItem("TLAD Pack", GiveWeaponPoolCue, GiveWeapon9mm, GiveWeaponAssaultShotgun, GiveWeaponMP5,
                                            GiveWeaponM4A1, GiveWeaponSniperRifle, GiveWeaponTLADGrenadeLauncher, GiveWeaponPipeBomb)
                    );
                weapItems.Add(
                        new Menu("TLAD Weapons",
                            new TextItem("Pool Cue", GiveWeaponPoolCue),
                            new TextItem("9mm", GiveWeapon9mm),
                            new TextItem("Shotgun1", GiveWeaponSawedOff),
                            new TextItem("Shotgun2", GiveWeaponAssaultShotgun),
                            new TextItem("Grenade Launcher", GiveWeaponTLADGrenadeLauncher),
                            new TextItem("Pipe Bomb", GiveWeaponPipeBomb)
                        )
                    );
            }
            if (Game.CurrentEpisode == GameEpisode.TBOGT)
            {
                weapPacks.Add(
                    new TextItem("TBOGT Pack", GiveWeaponBaseballBat, GiveWeaponPistol44, GiveWeaponAdvancedShotgun, GiveWeaponAdvancedSMG,
                                            GiveWeaponAdvancedMG, GiveWeaponAdvancedSniper, GiveWeaponTBOGTGrenadeLauncher, GiveWeaponStickyBomb)
                );

                weapItems.Add(
                    new Menu("TBOGT Weapons",
                        new TextItem(".44", GiveWeaponPistol44),
                        new TextItem("Advanced Shotgun", GiveWeaponAdvancedShotgun),
                        new TextItem("Explosive Shotgun", GiveWeaponExplosiveShotgun),
                        new TextItem("SMG", GiveWeaponAdvancedSMG),
                        new TextItem("Gold SMG", GiveWeaponGoldSMG),
                        new TextItem("HeavyMG", GiveWeaponAdvancedMG),
                        new TextItem("Sniper", GiveWeaponAdvancedSniper),
                        new TextItem("Grenade Launcher", GiveWeaponTBOGTGrenadeLauncher),
                        new TextItem("Sticky Bomb", GiveWeaponStickyBomb)
                    )
                );
            }

            weapItems.Insert(0, new Menu("Weapon Packs", weapPacks.ToArray()));
            //weapItems.Add(new ToggleItem("Infinite Ammo", "InfiniteAmmo"));
            weapItems.Add(new ToggleItem("No Reload", "NoReload"));
            weapItems.Add(new TextItem("Remove All Items", RemoveAllItems));

            weaponsMenu = new Menu("Weapons",
                weapItems.ToArray()
            );
        }
    }
}
