# Peebles - Un jeu d'arcade au style Pachinko

Peebles est un jeu d'arcade inspiré du Pachinko où vous lancez des billes qui tombent à travers différents obstacles pour atteindre des paniers et marquer des points.

[Jouer à Peebles](https://play.unity.com/en/games/e1541874-7a07-449d-866e-a06cf460ff2f/build)

## Concept du jeu

- Lâchez des "peebles" (billes) en cliquant avec la souris ou en appuyant sur la barre d'espace
- Regardez-les rebondir et tomber à travers différents obstacles
- Collectez des bonus pour obtenir des effets spéciaux
- Marquez des points lorsque les billes atterrissent dans les paniers
- Essayez d'obtenir le score le plus élevé possible !

## Contrôles

- **Souris (clic gauche)** ou **Barre d'espace** : Lâcher une bille à la position actuelle de la souris
- La position horizontale de la souris détermine où la bille sera lâchée

## Caractéristiques

- Système de points avec différentes valeurs selon les paniers (20, 50, 100 points)
- Obstacles en mouvement : moulins rotatifs et bumpers mobiles
- Bonus à collecter avec différents effets :
  - Bonus rose : Spawn multiple de billes
  - Bonus cyan : Billes supplémentaires dans votre réserve
  - Bonus jaune : Score doublé pendant quelques secondes

## Les 4 piliers de la Programmation Orientée Objet (POO) dans le jeu

### 1. Encapsulation

L'encapsulation est utilisée pour protéger les données et restreindre l'accès direct aux attributs de classe.

**Exemple concret** : Dans la classe `GameManager.cs`

```csharp
public int score { get; private set; } = 0; // Score est lisible publiquement mais modifiable uniquement dans cette classe
public int highScore { get; private set; } = 0; // Pareil pour highScore
public int ballsLeft { get; private set; } // Nombre de balles restantes est en lecture seule de l'extérieur
```

Ces propriétés peuvent être lues depuis n'importe quelle classe, mais seuls les méthodes internes de `GameManager` peuvent les modifier, garantissant que le contrôle des scores et des billes reste centralisé.

### 2. Héritage

L'héritage permet de créer de nouvelles classes qui réutilisent et étendent les fonctionnalités d'une classe existante.

**Exemple concret** : La classe abstraite `Bonus.cs` et ses sous-classes

```csharp
// Dans Bonus.cs
public abstract class Bonus : MonoBehaviour
{
    [SerializeField] protected float bonusDuration = 4f;
    
    private void OnTriggerEnter(Collider other) //INHERITANCE all bonus types inherit from this
    {
        if (other.CompareTag("Ball"))
        {
            ApplyBonus();
            Destroy(gameObject);
        }
    }
    
    // Méthode abstraite que chaque bonus spécifique implémentera
    protected abstract void ApplyBonus();
}
```

Les classes `BallSpawner.cs`, `ExtraBalls.cs` et `ScoreDoubler.cs` héritent toutes de cette classe de base `Bonus` et implémentent leur propre version de la méthode `ApplyBonus()`. Cela permet de réutiliser la logique commune tout en permettant des comportements spécifiques.

### 3. Polymorphisme

Le polymorphisme permet à des objets de classes différentes d'être traités comme des objets d'une même super-classe.

**Exemple concret** : Les différentes implémentations de `ApplyBonus()` dans les classes de bonus

```csharp
// Dans BallSpawner.cs
protected override void ApplyBonus()
{
    for (int i = 0; i < ballCount; i++)
    {
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
        gameManager.SetBallOnScreen(gameManager.GetBallOnScreen() + 1);
    }
}

// Dans ExtraBalls.cs
protected override void ApplyBonus() // POLYMORPHISM each bonus has individual effect
{
    int currentBalls = gameManager.GetBallsLeft();
    gameManager.SetBallsLeft(currentBalls + 5);
}

// Dans ScoreDoubler.cs
protected override void ApplyBonus()
{
    StartCoroutine(DoubleScore());
}
```

Chaque type de bonus redéfinit la méthode `ApplyBonus()` avec une implémentation spécifique. Lorsqu'une bille entre en collision avec un bonus, la méthode `OnTriggerEnter` de la classe de base appelle `ApplyBonus()`, et grâce au polymorphisme, c'est la version appropriée qui est exécutée selon le type de bonus.

### 4. Abstraction

L'abstraction consiste à masquer les détails complexes et à montrer uniquement les fonctionnalités nécessaires.

**Exemple concret** : Dans `BoxValidator.cs`

```csharp
private void OnTriggerEnter(Collider other)
{
    if (IsBall(other))
    {
        HandleBallEntry(other);
    }
}

private bool IsBall(Collider other)
{
    return other.CompareTag("Ball");
}

private void HandleBallEntry(Collider ball)
{
    UpdateScore();
    PlayPointSound();
    Destroy(ball.gameObject);
}
```

Cette classe abstrait les détails complexes de la gestion de la collision avec une bille en divisant le processus en méthodes plus petites et plus spécifiques. L'utilisateur de cette classe n'a pas besoin de connaître les détails de mise en œuvre de chaque étape.

## Technologie

- Développé avec Unity
- Code écrit en C#
- Utilise les principes de la Programmation Orientée Objet (POO)

---

# Peebles - Pachinko-style Arcade Game

Peebles is an arcade game inspired by Pachinko where you drop balls that fall through various obstacles to reach baskets and score points.

[Play Peebles](https://play.unity.com/en/games/e1541874-7a07-449d-866e-a06cf460ff2f/build)

## Game Concept

- Drop "peebles" (balls) by clicking with the mouse or pressing the space bar
- Watch them bounce and fall through various obstacles
- Collect bonuses for special effects
- Score points when balls land in baskets
- Try to get the highest score possible!

## Controls

- **Mouse (left click)** or **Space bar**: Drop a ball at the current mouse position
- The horizontal position of the mouse determines where the ball will be dropped

## Features

- Point system with different values depending on the baskets (20, 50, 100 points)
- Moving obstacles: rotating mills and moving bumpers
- Collectible bonuses with different effects:
  - Pink bonus: Multiple ball spawn
  - Cyan bonus: Extra balls in your reserve
  - Yellow bonus: Double score for a few seconds

## The 4 Pillars of Object-Oriented Programming (OOP) in the Game

### 1. Encapsulation

Encapsulation is used to protect data and restrict direct access to class attributes.

**Concrete example**: In the `GameManager.cs` class

```csharp
public int score { get; private set; } = 0; // Score is publicly readable but only writable within this class
public int highScore { get; private set; } = 0; // Same for highScore
public int ballsLeft { get; private set; } // BallsLeft is read-only from outside
```

These properties can be read from any class, but only internal methods of `GameManager` can modify them, ensuring that control of scores and balls remains centralized.

### 2. Inheritance

Inheritance allows creating new classes that reuse and extend the functionality of an existing class.

**Concrete example**: The abstract class `Bonus.cs` and its subclasses

```csharp
// In Bonus.cs
public abstract class Bonus : MonoBehaviour
{
    [SerializeField] protected float bonusDuration = 4f;
    
    private void OnTriggerEnter(Collider other) //INHERITANCE all bonus types inherit from this
    {
        if (other.CompareTag("Ball"))
        {
            ApplyBonus();
            Destroy(gameObject);
        }
    }
    
    // Abstract method that each specific bonus will implement
    protected abstract void ApplyBonus();
}
```

The classes `BallSpawner.cs`, `ExtraBalls.cs`, and `ScoreDoubler.cs` all inherit from this `Bonus` base class and implement their own version of the `ApplyBonus()` method. This allows reusing common logic while enabling specific behaviors.

### 3. Polymorphism

Polymorphism allows objects of different classes to be treated as objects of the same superclass.

**Concrete example**: The different implementations of `ApplyBonus()` in the bonus classes

```csharp
// In BallSpawner.cs
protected override void ApplyBonus()
{
    for (int i = 0; i < ballCount; i++)
    {
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
        gameManager.SetBallOnScreen(gameManager.GetBallOnScreen() + 1);
    }
}

// In ExtraBalls.cs
protected override void ApplyBonus() // POLYMORPHISM each bonus has individual effect
{
    int currentBalls = gameManager.GetBallsLeft();
    gameManager.SetBallsLeft(currentBalls + 5);
}

// In ScoreDoubler.cs
protected override void ApplyBonus()
{
    StartCoroutine(DoubleScore());
}
```

Each type of bonus overrides the `ApplyBonus()` method with a specific implementation. When a ball collides with a bonus, the `OnTriggerEnter` method of the base class calls `ApplyBonus()`, and thanks to polymorphism, the appropriate version is executed according to the type of bonus.

### 4. Abstraction

Abstraction consists of hiding complex details and showing only the necessary functionalities.

**Concrete example**: In `BoxValidator.cs`

```csharp
private void OnTriggerEnter(Collider other)
{
    if (IsBall(other))
    {
        HandleBallEntry(other);
    }
}

private bool IsBall(Collider other)
{
    return other.CompareTag("Ball");
}

private void HandleBallEntry(Collider ball)
{
    UpdateScore();
    PlayPointSound();
    Destroy(ball.gameObject);
}
```

This class abstracts the complex details of handling a collision with a ball by breaking the process down into smaller, more specific methods. The user of this class doesn't need to know the implementation details of each step.

## Technology

- Developed with Unity
- Code written in C#
- Uses the principles of Object-Oriented Programming (OOP)
