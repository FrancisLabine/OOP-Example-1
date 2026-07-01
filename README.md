# Production Chain Simulation

A WPF/.NET simulation of a configurable production chain. The project models raw material plants, production plants, assembly plants, warehouses, transport between buildings, and warehouse selling strategies.

This repository is intended to demonstrate object-oriented design fundamentals in a small but structured application: inheritance, polymorphism, interfaces, factories, strategies, observer-style notifications, XML configuration, and automated tests.

## Project Structure

```text
SimulationApp.Core    Domain model, simulation loop, XML loading, strategies
SimulationApp.UI      WPF application and rendering layer
SimulationApp.Tests   NUnit tests for XML loading and simulation behavior
.github/workflows     GitHub Actions CI workflow
```

## Architecture

The solution is split into three projects:

- `SimulationApp.Core` contains the simulation model and business rules. It has no dependency on WPF.
- `SimulationApp.UI` renders the simulation and maps domain state to visual assets.
- `SimulationApp.Tests` verifies configuration loading and core simulation behavior.

The simulation is loaded from XML configuration files. Metadata defines building types, inputs, outputs, production intervals, and UI asset paths. The simulation section defines building instances and pathways between them.

## OOP Concepts Demonstrated

- **Abstraction:** shared behavior is modeled through `BuildingBase` and `PlantBase`.
- **Inheritance:** raw material, production, assembly, and warehouse buildings specialize common building behavior.
- **Polymorphism:** the simulation loop executes buildings through the common `BuildingBase` contract.
- **Factory Pattern:** XML building types are converted into domain objects by `BuildingFactory`.
- **Strategy Pattern:** warehouses use interchangeable selling strategies.
- **Observer-style notifications:** downstream buildings signal upstream buildings to start or stop production based on capacity.
- **Separation of concerns:** the core model exposes simulation state while the UI handles rendering.

## Running the Application

```powershell
dotnet run --project SimulationApp.UI\SimulationApp.UI.csproj
```

In Visual Studio, open `OOP-Example-1.sln`, set `SimulationApp.UI` as the startup project, and run.

## Running Tests

```powershell
dotnet test SimulationApp.Tests\SimulationApp.Tests.csproj
```

## Continuous Integration

GitHub Actions runs restore, build, and tests on every push or pull request to `main`.

## Current Test Coverage

The test suite verifies:

- XML metadata and building parsing
- Path loading and building links
- Warehouse status behavior
- Upstream production signaling
- Raw material production
- Component delivery from transport to inventory

## Future Improvements

- Add more deterministic strategy tests.
- Add a richer UI control model for pause/resume/reset.
- Replace XML with a versioned configuration format if the simulation grows.
