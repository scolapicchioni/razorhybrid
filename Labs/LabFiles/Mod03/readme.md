# Goals

## Dependency Injection

- Use CLEAN architecture: three projects for
    - **Core** (with Entities, Interfaces and Services)
    - **Infrastructure** (with repository implementations)
    - **Web** (with Razor Pages)
- Build the Service for the Photo to Get all Photos, Add a Photo, Get One Photo By Id.
- Let the Service depend on a Repository 
- Let the Service and Repository implement interfaces
- Register the interfaces and classes in the Service Container