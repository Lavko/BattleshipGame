using System.Numerics;
using BattleshipGame;
using BattleshipGame.Renderers;
using BattleshipGame.States;

const bool showShips = false;

var initializer = new GameInitializer();

var state = new MemoryGameState(
    initializer.InitializeEmptyBoard(),
    initializer.InitializeShips(),
    new List<Vector2>()
);

var renderer = new ConsoleGameRenderer(showShips);

new GameController(state, renderer).Start();
