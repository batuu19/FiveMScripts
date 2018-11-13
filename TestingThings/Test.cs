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
        

        public Test()
        {
            Tick += OnTick;
            try
            {
                EventHandlers["chatMessage"] += new Action<string, List<object>, string>(OnChatMessage);
                Debug.WriteLine("Registered Command");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error in jumper constructor: {e}");
            }
        }


        private async Task OnTick()
        {
            try
            {
                var player = Game.PlayerPed;
                if(Game.IsControlJustPressed(0,Control.Jump))
                {
                    player.ApplyForce(new Vector3(0,0,forceMultipier));
                    DrawNotification("Force added");
                }

                player.IsCollisionProof = true;
                if(player.IsRagdoll)
                {
                    player.CancelRagdoll();
                }
                
            }
            catch (Exception e)
            {
                DrawNotification($"Jumper error: {e}");
            }
        }

        private void OnChatMessage(string author, List<object> color, string text)
        {
            try
            {
                if (text[0] == '/')
                    ExecuteCommand(text.Substring(1));
            }
            catch (Exception e)
            {
                DrawNotification($"Car not created: {e}");
            }
        }

        private async void ExecuteCommand(string command)
        {
            int spaceIndex = command.IndexOf(" ");
            string commandName = command.Substring(0, spaceIndex);
            string arg0 = command.Substring(spaceIndex, command.Length - spaceIndex).Trim();
            switch (commandName)
            {
                case "car":
                    {
                        Vector3 vehPos = Game.PlayerPed.GetOffsetPosition(new Vector3(0f, 8f, 0.5f));
                        string veh = arg0;
                        uint vehicleHash = (uint)API.GetHashKey(veh);
                        API.RequestModel(vehicleHash);

                        int i = 0;
                        while (++i < 2000 && !API.HasModelLoaded(vehicleHash))
                            await Delay(1);
                        if (i < 2000)
                        {
                            API.CreateVehicle(vehicleHash, vehPos.X, vehPos.Y, vehPos.Z, API.GetEntityHeading(API.PlayerPedId()) + 90, true, false);
                            DrawNotification("Created car");
                        }
                        else DrawNotification("Could not load car");

                        break;
                    }
                case "summon":
                    {
                        //string entity = arg0;
                        //uint entityHash = (uint)API.GetHashKey(entity);
                        //if(API.IsModelValid(entityHash))API.RequestModel(entityHash);
                        //else
                        //{
                        //    DrawNotification("model not valid");
                        //    break;
                        //}
                        //int i = 0;
                        //while (++i < 2000 && !API.HasModelLoaded(entityHash))
                        //    await Delay(1);
                        //if (i < 2000)
                        //{
                        //    API.createModel
                        //    DrawNotification("Created car");
                        //}
                        //else DrawNotification("Could not load entity");
                        ///not working.......========================================
                        int count;
                        int.TryParse(arg0,out count);
                        if (count > 1) count = 1;
                        Vector3 vec = Game.PlayerPed.GetOffsetPosition(new Vector3(0f, 8f, 0.5f));
                        for (int i = 0; i < count; i++)
                        {
                            API.CreateRandomPed(vec.X + i, vec.Y, vec.Z);
                        }
                        break;
                    }
                default:
                    DrawNotification($"command {commandName} not existing yet");
                    break;
            }
        }

        private void DrawNotification(string text, bool blink = true)
        {
            API.SetNotificationTextEntry("STRING");
            API.AddTextComponentString(text);
            API.DrawNotification(blink, false);
        }
        
    }
}
