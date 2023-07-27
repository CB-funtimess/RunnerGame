using System.ComponentModel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameClasses.GameStates;
using Engine.GameStates;

namespace GameClasses.Managers;

public class ScreenManager
{
    #region Fields
    private MenuState menu;
    private PlayState play;
    private PauseState pause;
    private GameState current;
    private Game game;
    #endregion

    #region Properties
    public Rectangle WindowDimensions { get; set; }
    #endregion

    #region Methods
    public ScreenManager(Game game)
    {
        this.game = game;
        menu = new MenuState(game);
        menu.Play += Play_Click;
        //menu.Options += Options_Click;

        play = new PlayState();
        pause = new PauseState();

        current = menu;
    }

    private void Play_Click(object sender, EventArgs e)
    {
        current = play;
    }

    public void Initialize()
    {
        menu.Initialize();
        play.Initialize();
        pause.Initialize();
    }

    public void LoadContent()
    {
        menu.LoadContent();
        play.LoadContent();
        pause.LoadContent();
    }

    public void Update(GameTime gameTime)
    {
        current.Update(gameTime);
    }
    public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
    {
        current.Draw(gameTime, _spriteBatch);
    }
    #endregion
}