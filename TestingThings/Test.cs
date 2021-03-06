﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace TestingThings
{
    public class Test : BaseScript
    {
        private float forceMultipier = 50f;
        private bool jumper = false;

        #region Models
        string[] pedModels =
        {
        "A_F_M_Beach_01",
        "A_F_M_BevHills_01",
        "A_F_M_BevHills_02",
        "A_F_M_BodyBuild_01",
        "A_F_M_Business_02",
        "A_F_M_Downtown_01",
        "A_F_M_EastSA_01",
        "A_F_M_EastSA_02",
        "A_F_M_FatBla_01",
        "A_F_M_FatCult_01",
        "A_F_M_FatWhite_01",
        "A_F_M_KTown_01",
        "A_F_M_KTown_02",
        "A_F_M_ProlHost_01",
        "A_F_M_Salton_01",
        "A_F_M_SkidRow_01",
        "A_F_M_SouCentMC_01",
        "A_F_M_SouCent_01",
        "A_F_M_SouCent_02",
        "A_F_M_Tourist_01",
        "A_F_M_TrampBeac_01",
        "A_F_M_Tramp_01",
        "A_F_O_GenStreet_01",
        "A_F_O_Indian_01",
        "A_F_O_KTown_01",
        "A_F_O_Salton_01",
        "A_F_O_SouCent_01",
        "A_F_O_SouCent_02",
        "A_F_Y_Beach_01",
        "A_F_Y_BevHills_01",
        "A_F_Y_BevHills_02",
        "A_F_Y_BevHills_03",
        "A_F_Y_BevHills_04",
        "A_F_Y_Business_01",
        "A_F_Y_Business_02",
        "A_F_Y_Business_03",
        "A_F_Y_Business_04",
        "A_F_Y_EastSA_01",
        "A_F_Y_EastSA_02",
        "A_F_Y_EastSA_03",
        "A_F_Y_Epsilon_01",
        "A_F_Y_Fitness_01",
        "A_F_Y_Fitness_02",
        "A_F_Y_GenHot_01",
        "A_F_Y_Golfer_01",
        "A_F_Y_Hiker_01",
        "A_F_Y_Hippie_01",
        "A_F_Y_Hipster_01",
        "A_F_Y_Hipster_02",
        "A_F_Y_Hipster_03",
        "A_F_Y_Hipster_04",
        "A_F_Y_Indian_01",
        "A_F_Y_Juggalo_01",
        "A_F_Y_Runner_01",
        "A_F_Y_RurMeth_01",
        "A_F_Y_SCDressy_01",
        "A_F_Y_Skater_01",
        "A_F_Y_SouCent_01",
        "A_F_Y_SouCent_02",
        "A_F_Y_SouCent_03",
        "A_F_Y_Tennis_01",
        "A_F_Y_Topless_01",
        "A_F_Y_Tourist_01",
        "A_F_Y_Tourist_02",
        "A_F_Y_Vinewood_01",
        "A_F_Y_Vinewood_02",
        "A_F_Y_Vinewood_03",
        "A_F_Y_Vinewood_04",
        "A_F_Y_Yoga_01",
        "A_M_M_ACult_01",
        "A_M_M_AfriAmer_01",
        "A_M_M_Beach_01",
        "A_M_M_Beach_02",
        "A_M_M_BevHills_01",
        "A_M_M_BevHills_02",
        "A_M_M_Business_01",
        "A_M_M_EastSA_01",
        "A_M_M_EastSA_02",
        "A_M_M_Farmer_01",
        "A_M_M_FatLatin_01",
        "A_M_M_GenFat_01",
        "A_M_M_GenFat_02",
        "A_M_M_Golfer_01",
        "A_M_M_HasJew_01",
        "A_M_M_Hillbilly_01",
        "A_M_M_Hillbilly_02",
        "A_M_M_Indian_01",
        "A_M_M_KTown_01",
        "A_M_M_Malibu_01",
        "A_M_M_MexCntry_01",
        "A_M_M_MexLabor_01",
        "A_M_M_OG_Boss_01",
        "A_M_M_Paparazzi_01",
        "A_M_M_Polynesian_01",
        "A_M_M_ProlHost_01",
        "A_M_M_RurMeth_01",
        "A_M_M_Salton_01",
        "A_M_M_Salton_02",
        "A_M_M_Salton_03",
        "A_M_M_Salton_04",
        "A_M_M_Skater_01",
        "A_M_M_Skidrow_01",
        "A_M_M_SoCenLat_01",
        "A_M_M_SouCent_01",
        "A_M_M_SouCent_02",
        "A_M_M_SouCent_03",
        "A_M_M_SouCent_04",
        "A_M_M_StLat_02",
        "A_M_M_Tennis_01",
        "A_M_M_Tourist_01",
        "A_M_M_TrampBeac_01",
        "A_M_M_Tramp_01",
        "A_M_M_TranVest_01",
        "A_M_M_TranVest_02",
        "A_M_O_ACult_01",
        "A_M_O_ACult_02",
        "A_M_O_Beach_01",
        "A_M_O_GenStreet_01",
        "A_M_O_KTown_01",
        "A_M_O_Salton_01",
        "A_M_O_SouCent_01",
        "A_M_O_SouCent_02",
        "A_M_O_SouCent_03",
        "A_M_O_Tramp_01",
        "A_M_Y_ACult_01",
        "A_M_Y_ACult_02",
        "A_M_Y_BeachVesp_01",
        "A_M_Y_BeachVesp_02",
        "A_M_Y_Beach_01",
        "A_M_Y_Beach_02",
        "A_M_Y_Beach_03",
        "A_M_Y_BevHills_01",
        "A_M_Y_BevHills_02",
        "A_M_Y_BreakDance_01",
        "A_M_Y_BusiCas_01",
        "A_M_Y_Business_01",
        "A_M_Y_Business_02",
        "A_M_Y_Business_03",
        "A_M_Y_Cyclist_01",
        "A_M_Y_DHill_01",
        "A_M_Y_Downtown_01",
        "A_M_Y_EastSA_01",
        "A_M_Y_EastSA_02",
        "A_M_Y_Epsilon_01",
        "A_M_Y_Epsilon_02",
        "A_M_Y_Gay_01",
        "A_M_Y_Gay_02",
        "A_M_Y_GenStreet_01",
        "A_M_Y_GenStreet_02",
        "A_M_Y_Golfer_01",
        "A_M_Y_HasJew_01",
        "A_M_Y_Hiker_01",
        "A_M_Y_Hippy_01",
        "A_M_Y_Hipster_01",
        "A_M_Y_Hipster_02",
        "A_M_Y_Hipster_03",
        "A_M_Y_Indian_01",
        "A_M_Y_Jetski_01",
        "A_M_Y_Juggalo_01",
        "A_M_Y_KTown_01",
        "A_M_Y_KTown_02",
        "A_M_Y_Latino_01",
        "A_M_Y_MethHead_01",
        "A_M_Y_MexThug_01",
        "A_M_Y_MotoX_01",
        "A_M_Y_MotoX_02",
        "A_M_Y_MusclBeac_01",
        "A_M_Y_MusclBeac_02",
        "A_M_Y_Polynesian_01",
        "A_M_Y_RoadCyc_01",
        "A_M_Y_Runner_01",
        "A_M_Y_Runner_02",
        "A_M_Y_Salton_01",
        "A_M_Y_Skater_01",
        "A_M_Y_Skater_02",
        "A_M_Y_SouCent_01",
        "A_M_Y_SouCent_02",
        "A_M_Y_SouCent_03",
        "A_M_Y_SouCent_04",
        "A_M_Y_StBla_01",
        "A_M_Y_StBla_02",
        "A_M_Y_StLat_01",
        "A_M_Y_StWhi_01",
        "A_M_Y_StWhi_02",
        "A_M_Y_Sunbathe_01",
        "A_M_Y_Surfer_01",
        "A_M_Y_VinDouche_01",
        "A_M_Y_Vinewood_01",
        "A_M_Y_Vinewood_02",
        "A_M_Y_Vinewood_03",
        "A_M_Y_Vinewood_04",
        "A_M_Y_Yoga_01",
        "G_F_Y_ballas_01",
        "G_F_Y_Families_01",
        "G_F_Y_Lost_01",
        "G_F_Y_Vagos_01",
        "G_M_M_ArmBoss_01",
        "G_M_M_ArmGoon_01",
        "G_M_M_ArmLieut_01",
        "G_M_M_ChemWork_01",
        "G_M_M_ChiBoss_01",
        "G_M_M_ChiCold_01",
        "G_M_M_ChiGoon_01",
        "G_M_M_ChiGoon_02",
        "G_M_M_KorBoss_01",
        "G_M_M_MexBoss_01",
        "G_M_M_MexBoss_02",
        "G_M_Y_ArmGoon_02",
        "G_M_Y_Azteca_01",
        "G_M_Y_BallaEast_01",
        "G_M_Y_BallaOrig_01",
        "G_M_Y_BallaSout_01",
        "G_M_Y_FamCA_01",
        "G_M_Y_FamDNF_01",
        "G_M_Y_FamFor_01",
        "G_M_Y_Korean_01",
        "G_M_Y_Korean_02",
        "G_M_Y_KorLieut_01",
        "G_M_Y_Lost_01",
        "G_M_Y_Lost_02",
        "G_M_Y_Lost_03",
        "G_M_Y_MexGang_01",
        "G_M_Y_MexGoon_01",
        "G_M_Y_MexGoon_02",
        "G_M_Y_MexGoon_03",
        "G_M_Y_PoloGoon_01",
        "G_M_Y_PoloGoon_02",
        "G_M_Y_SalvaBoss_01",
        "G_M_Y_SalvaGoon_01",
        "G_M_Y_SalvaGoon_02",
        "G_M_Y_SalvaGoon_03",
        "G_M_Y_StrPunk_01",
        "G_M_Y_StrPunk_02",
        "HC_Driver",
        "HC_Gunman",
        "HC_Hacker",
        "IG_Abigail",
        "IG_AmandaTownley",
        "IG_Andreas",
        "IG_Ashley",
        "IG_BallasOG",
        "IG_Bankman",
        "IG_Barry",
        "IG_BestMen",
        "IG_Beverly",
        "IG_Brad",
        "IG_Bride",
        "IG_Car3guy1",
        "IG_Car3guy2",
        "IG_Casey",
        "IG_Chef",
        "IG_ChengSr",
        "IG_ChrisFormage",
        "IG_Clay",
        "IG_ClayPain",
        "IG_Cletus",
        "IG_Dale",
        "IG_DaveNorton",
        "IG_Denise",
        "IG_Devin",
        "IG_Dom",
        "IG_Dreyfuss",
        "IG_DrFriedlander",
        "IG_Fabien",
        "IG_FBISuit_01",
        "IG_Floyd",
        "IG_Groom",
        "IG_Hao",
        "IG_Hunter",
        "IG_Janet",
        "ig_JAY_Norris",
        "IG_JewelAss",
        "IG_JimmyBoston",
        "IG_JimmyDiSanto",
        "IG_JoeMinuteMan",
        "ig_JohnnyKlebitz",
        "IG_Josef",
        "IG_Josh",
        "IG_KerryMcIntosh",
        "IG_LamarDavis",
        "IG_LesterCrest",
        "IG_LifeInvad_01",
        "IG_LifeInvad_02",
        "IG_Magenta",
        "IG_Manuel",
        "IG_Marnie",
        "IG_MaryAnn",
        "IG_Maude",
        "IG_Michelle",
        "IG_Milton",
        "IG_Molly",
        "IG_MRK",
        "IG_MrsPhillips",
        "IG_MRS_Thornhill",
        "IG_Natalia",
        "IG_NervousRon",
        "IG_Nigel",
        "IG_Old_Man1A",
        "IG_Old_Man2",
        "IG_Omega",
        "IG_ONeil",
        "IG_Orleans",
        "IG_Ortega",
        "IG_Paper",
        "IG_Patricia",
        "IG_Priest",
        "IG_ProlSec_02",
        "IG_Ramp_Gang",
        "IG_Ramp_Hic",
        "IG_Ramp_Hipster",
        "IG_Ramp_Mex",
        "IG_RoccoPelosi",
        "IG_RussianDrunk",
        "IG_Screen_Writer",
        "IG_SiemonYetarian",
        "IG_Solomon",
        "IG_SteveHains",
        "IG_Stretch",
        "IG_Talina",
        "IG_Tanisha",
        "IG_TaoCheng",
        "IG_TaosTranslator"
        };
        #endregion

        public Test()
        {
            API.RegisterCommand("jumper", new Action<int, List<object>, string>(ToggleJumper), false);
            API.RegisterCommand("spawn", new Action<int, List<object>, string>(SpawnCar), false);
            API.RegisterCommand("wanted", new Action<int, List<object>, string>(SetWantedLevel), false);
            API.RegisterCommand("peds", new Action<int, List<object>, string>(SpawnPeds), false);
            Tick += OnTick;

        }

        #region OnTick
        private async Task OnTick()
        {
            try
            {
                var player = Game.PlayerPed;
                if (Game.IsControlJustPressed(0, Control.Jump) && jumper)
                {
                    player.ApplyForce(new Vector3(0, 0, forceMultipier));
                    DrawNotification("Force added");
                }

                if (player.IsRagdoll)
                {
                    player.CancelRagdoll();
                }
                player.IsCollisionProof = true;
            }
            catch (Exception e)
            {
                DrawNotification($"Jumper error: {e}");
            }

        }
        #endregion
        #region Notifications
        private void DrawNotification(string text, bool blink = true)
        {
            API.SetNotificationTextEntry("STRING");
            API.AddTextComponentString(text);
            API.DrawNotification(blink, false);
        }
        private void ShowChatNotification(string text)
        {
            TriggerEvent("chatMessage", "", new int[] { 0, 0, 0 }, text);
        }
        #endregion
        #region CommandsHandlers
        private void ToggleJumper(int source, List<object> args, string raw)
        {
            jumper = !jumper;
            ShowChatNotification("jumper " + (jumper ? "on" : "off"));
        }
        private async void SpawnCar(int source, List<object> args, string raw)
        {
            Vector3 vehPos = Game.PlayerPed.GetOffsetPosition(new Vector3(0f, 8f, 0.5f));
            string veh = (string)args[0];
            uint vehicleHash = (uint)API.GetHashKey(veh);
            API.RequestModel(vehicleHash);

            int i = 0;
            while (++i < 2000 && !API.HasModelLoaded(vehicleHash))
                await Delay(1);
            if (i < 2000)
            {
                API.CreateVehicle(vehicleHash, vehPos.X, vehPos.Y, vehPos.Z, API.GetEntityHeading(API.PlayerPedId()) + 90, true, false);
                ShowChatNotification("Created car");
            }
            else ShowChatNotification("Could not load car");

        }
        private void SetWantedLevel(int source, List<object> args, string raw)
        {
            int level = int.Parse((string)args[0]);
            ShowChatNotification($"level {level}");
            Function.Call(Hash.SET_PLAYER_WANTED_LEVEL, Game.Player.Handle, level, false);
        }
        private async void SpawnPeds(int source, List<object> args, string raw)
        {
            ShowChatNotification("Start spawn peds");
            try
            {
                Random rnd = new Random();
                int pedsCount = 1;
                pedsCount = int.Parse((string)args[0]);


                if (pedsCount < 0) pedsCount = 1;
                /*
                 * Ped types
                 * Michael = 0
                 * Franklin = 1
                 * Trevor = 2
                 * Army = 29
                 * Animal = 28
                 * SWAT = 27
                 * LSFD = 21
                 * Paramedic = 20
                 * Cop = 6
                 * Male = 4
                 * Female = 5 
                 * Human = 26
                 */
                int pedType = 26;
                Vector3 vec = Game.PlayerPed.GetOffsetPosition(new Vector3(0f, 8f, 0.5f));
                uint modelHash;


                for (int j = 0; j < pedsCount; j++)
                {
                    modelHash = (uint)API.GetHashKey(pedModels[rnd.Next(0, pedModels.Length)]);
                    API.RequestModel(modelHash);
                    int i = 0;
                    while (++i < 2000 && !API.HasModelLoaded(modelHash))
                        await Delay(1);
                    if (i < 2000)
                    {
                        API.CreatePed(pedType, modelHash, vec.X, vec.Y + j, vec.Z, API.GetEntityHeading(API.PlayerPedId()) + 90, false, false);
                        ShowChatNotification("Loaded ped");
                    }
                    else ShowChatNotification("Could not load ped");
                    
                }
                ShowChatNotification("Spawned peds");
            }
            catch (Exception e)
            {
                ShowChatNotification($"peds error: {e}");
            }
        }
        #endregion
    }
}
