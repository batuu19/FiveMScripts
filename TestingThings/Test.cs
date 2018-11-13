using System;
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
                Vector3 vec = Game.PlayerPed.GetOffsetPosition(new Vector3(0f, 8f, 0.5f));
                for (int j = 0; j < pedsCount; j++)
                {
                    API.CreatePed(26, (uint)Game.PlayerPed.Model.Hash, vec.X, vec.Y + j, vec.Z, API.GetEntityHeading(API.PlayerPedId()) + 90, false, false);
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
