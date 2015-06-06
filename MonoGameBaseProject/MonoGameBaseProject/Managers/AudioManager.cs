using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace EcoShoot.Managers
{
    public class AudioManager //SINGLETON
    {
        static AudioManager instance;
        public static AudioManager Instance 
        {
            get 
            {
                if (instance == null)
                    instance = new AudioManager();

                return instance;
            }
        }

        //Constructor privado
        public AudioManager() 
        {

        }

        public void LoadContent(ContentManager Content)
        {
            
        }

        /* Reproduce un sonido
         * */
        public void PlaySound(SoundEffect sound)
        {
            sound.Play();
        }
    }
}
