# OOP-Example-1


## Overview
The simulation represents an environment where different types of buildings interact to simulate a production chain. These buildings include:
- **Raw Material Plants**
- **Production Plants**
- **Assembly Plants**
- **Warehouses**
- **Pathways for movement**
- **Components representing items in transit**

Each building has its own responsibilities and behavior, and the system uses polymorphism, inheritance, and abstraction to enable flexible simulation logic.


## Key Concepts

### OOP Principles and Programming Best Practices
- **Abstraction**: Buildings and components are abstracted into classes with specific responsibilities.
- **Encapsulation**: Each building encapsulates its own state and behavior, allowing for clear interfaces.
- **Inheritance**: Common functionality is shared through base classes, allowing for code reuse.
- **Polymorphism**: Buildings can be treated as their base type, allowing for flexible interactions.
- **Composition**: Buildings can contain other buildings or components, allowing for complex structures.
- **Interfaces**: Used to define contracts for behaviors (e.g., IObserver, ISellingStrategy).
- **Dependency Injection**: Used to inject dependencies like selling strategies into warehouses, promoting loose coupling.
- **Single Responsibility Principle**: Each class has a single responsibility, making the code easier to maintain and extend.
- **High Cohesion**: Classes are designed to have closely related functionalities, making them easier to understand and maintain.
- **Low Coupling**: Classes interact through well-defined interfaces, reducing dependencies and making the system more modular.

### Design Patterns
- **Template Method Pattern**: Used in buildings to define a skeleton of the production process, allowing subclasses to implement specific steps.
- **Factory Pattern**: Used to create different types of buildings and components dynamically.
- **Observer Pattern**: Buildings can observe changes in production and react accordingly.
- **Strategy Pattern**: Warehouses can use different selling strategies to manage how they sell components.

### Building Hierarchy
- **BuildingBase**: Abstract base class for all buildings.
- **PlantBase**: Extends BuildingBase, adds production-related logic.
- **RawMatPlant**, **ProductionPlant**, **AssemblyPlant**: Specific plant types implementing factory logic.
- **Warehouse**: Stores and sells produced components using a pluggable strategy.

### Component Flow
- **Component**: Represents a unit of production (e.g., metal, engine).
- Components move between buildings and are processed based on their ProductionType.

### Observer Pattern
-Buildings can observe and react to production events (e.g., notify start/stop).
-Implemented via the **IObserver** interface.

### Strategies
- **ISellingStrategy**: Interface for implementing different selling strategies (e.g., **BulkStrategy**, **RandomStrategy**).
- Promotes open/closed principle by allowing warehouses to behave differently based on strategy.

### Architectural Patterns
- **Layered Architecture**: The system is organized into layers (e.g., data layer, business logic layer) to separate concerns.
- **Event-Driven Architecture**: Buildings can react to events in the system, allowing for dynamic interactions.
- **MVVM (Model-View-ViewModel)**: The simulation can be extended to use MVVM for UI interactions, separating the view logic from the business logic.

### Documentation
 All UML diagrams and architecture decisions are in the [Doc](https://github.com/FrancisLabine/OOP-Example-1/tree/main/Doc) folder.

### Future Improvements
- Implement more selling strategies or production rules
- Add more building types, component types, pathways, and interactions
- Add weight and size attributes to components for more realistic simulation