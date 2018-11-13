using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace WeaponSpawner
{
    public class WeaponSpawner : BaseScript
    {
        public WeaponSpawner()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            try
            {
                foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
                {
                    if (!Game.PlayerPed.Weapons.HasWeapon(weapon))
                    {
                        Game.PlayerPed.Weapons.Give(weapon, 250, false, true);
                    }
                }

                await Delay(5000);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex}");
            }
        }
    }
}
