# Enterprise Architecture Assignment

This repository contains the deliverables for the Enterprise Architecture assignment. The project demonstrates the implementation of various architectural concepts and patterns in a microservices-based system.

## Deliverables

Below you’ll find the deliverables used to assess the assignment. Each deliverable is explained and motivated based on the choices made.

### 1. Backlog

A backlog with functional requirements, non-functional requirements, and architectural constraints, based on assumptions or literature.

### 2. Context Map

A context map with a list of domain events.

### 3. ArchiMate Model

An ArchiMate model of the enterprise architecture.

### 4. Implementation

The implementation of the enterprise architecture using the technology of your choice.

### 5. Postman Scripts

Postman scripts that trigger the various RESTful web APIs to demonstrate the functionality. There's no need to create a GUI.

### 6. Group Video Presentation

A group video presentation (max. 15 minutes) to convince the executive board of the value of the deliverables for their business.

### 7. Concept Application Document

A document describing where the following concepts have been applied in the solution and why they have been applied there. All mentioned concepts are compulsory:
- Microservices based on the principles of Domain-Driven Design (DDD)
- Eventual Consistency
- Event-Driven Architecture based on Messaging
- Command Query Responsibility Segregation (CQRS)
- Event Sourcing
- Enterprise Integration Patterns (to integrate with the given source of customers)
- Containerization of the Implementation

## Getting Started

To get started with the project, clone the repository and follow the instructions in the respective directories for each deliverable.

```bash
git clone https://github.com/TophoStan/SolutionArchitecture.git
```


Docker command to start RabbitMQ bus
```bash
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
```
