using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EcoShoot.Managers
{
    public class InputManager
    {
        static InputManager instance;
        KeyboardState newKeyboardState;
        KeyboardState oldKeyboardState;
        MouseState oldMouseState;
        MouseState newMouseState;

        private InputManager() { }

        public static InputManager Instance
        {
            get
            {
                //Si instance es null, crea una nueva instancia usando el constructor privado
                if (instance == null)
                {
                    //Se instancia desde el constructor
                    instance = new InputManager();
                }
                return instance;
            }
        }

        public void Update()
        {
            oldKeyboardState = newKeyboardState;
            if (!ScreenManager.Instance.isTransitioning)
                newKeyboardState = Keyboard.GetState();

            oldMouseState = newMouseState;
            if (!ScreenManager.Instance.isTransitioning)
                newMouseState = Mouse.GetState();

        }

        public Boolean KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (oldKeyboardState.IsKeyUp(key) && newKeyboardState.IsKeyDown(key))
                    return true;
            }

            return false;
        }

        public Boolean KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (oldKeyboardState.IsKeyDown(key) && newKeyboardState.IsKeyUp(key))
                    return true;
            }

            return false;
        }

        public Boolean KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (oldKeyboardState.IsKeyDown(key))
                    return true;
            }

            return false;
        }

        public Boolean LeftMouseButtonPressed() 
        {
            if (oldMouseState.LeftButton == ButtonState.Released &&
                newMouseState.LeftButton == ButtonState.Pressed)
                return true;

            else 
                return false;
        }

        public Boolean LeftMouseButtonReleased() 
        {
            if (oldMouseState.LeftButton == ButtonState.Pressed &&
                newMouseState.LeftButton == ButtonState.Released)
                return true;

            else
                return false;
        }

        public Boolean RightMouseButtonPressed() 
        {
            if (oldMouseState.RightButton == ButtonState.Released &&
                newMouseState.RightButton == ButtonState.Pressed)
                return true;

            else
                return false;
        }

        public Boolean RightMouseButtonReleased() 
        {
            if (oldMouseState.RightButton == ButtonState.Released &&
                newMouseState.RightButton == ButtonState.Pressed)
                return true;

            else
                return false;
        }
    }
}
