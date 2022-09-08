Design Patterns utilisés:

Strategy Pattern (Collisions)
Models/ILabyrinthComponent.cs
Models/Wall.cs
Models/LabyrinthComponent.cs
Models/Door.cs
Models/Gems.cs
Models/MiniSlime.cs

Flyweight (pour images)
Models/Image2D.cs
Models/SpriteFactory.cs

Singleton + Factory (pour gérérer des objets flyweight)
Models/SpriteFactory.cs
Models/Image2D.cs

Factory (Génération des objets composant le labyrinthe)
Models/LabyrinthComponentFactory.cs

Observer + Facade(Pour notifier entre View, Controllers et Models)
Models/IObservable.cs
Models/IObserver.cs
Models/Collection.cs
Models/Unsuscriber.cs
Controllers/HeaderController.cs
Views/HeaderView.cs

Adapter (Convertir les sections de la TileSheet en type d'image)
Models/SpriteToImageTypes.cs


