# Template .NET Core

[![Build](https://github.com/carlosdaniiel07/template-net-core/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/carlosdaniiel07/template-net-core/actions/workflows/main.yml)

Template base para criação de projetos back-end com ASP.NET Core 8.x

## 📌 Menu

- [Template .NET Core](#template-net-core)
  - [📌 Menu](#-menu)
  - [🚀 Tecnologias](#-tecnologias)
  - [📕 Metodologias e Princípios](#-metodologias-e-princípios)
  - [📐 Arquitetura do projeto](#-arquitetura-do-projeto)
  - [🔷 Conventional Commits](#-conventional-commits)

## 🚀 Tecnologias

- C#
- .NET Core 8
- Entity Framework Core
- AutoMapper
- BCrypt
- Swagger
- xUnit
- Fluent Assertions
- Moq
- AutoFixture

## 📕 Metodologias e Princípios

- Single Responsibility Principle (SRP)
- Dependency Inversion Principle (DIP)
- Don't Repeat Yourself (DRY)
- You Aren't Gonna Need It (YAGNI)
- Keep It Simple, Silly (KISS)
- Clean Architecture
- TDD
- Conventional Commits
- GitFlow
- Use Cases
- Continuous Integration
- Continuous Delivery

## 📐 Arquitetura do projeto

![](https://miro.medium.com/max/800/1*0R0r00uF1RyRFxkxo3HVDg.png)

Com o objetivo de construir uma aplicação organizada, escalável e desacoplada foi optado por implementar o conceito de _Clean Architecture_ (arquitetura limpa). Portanto, as regras de negócio, o acesso a dados e os controladores estão localizados em camadas distintas, consequentemente mantendo um alto nível de desacoplamento.

📚 Leia mais sobre arquitetura limpa:

- 🔗 [Descomplicando a Clean Architecture](https://medium.com/luizalabs/descomplicando-a-clean-architecture-cf4dfc4a1ac6)
- 🔗 [The Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

## 🔷 Conventional Commits

O projeto segue a especificação _Conventional Commits_, que determina uma série de regras para as mensagens de commit. Essa convenção possibilita um histórico de commits explicito, legível e de fácil compreensão. 

Em resumo, a especificação diz que é necessário adicionar um prefixo a mensagem de commit. Esse prefixo tem como papel identificar qual é o tipo do commit (uma nova funcionalidade, uma correção de bug, etc.)
  - `feat`: nova feature
  - `fix`: correção de bug
  - `chore`: dependência (NPM, NuGet, etc.) ou arquivo de configuração do projeto
  - `ci`: CI e CD
  - `refactor`: refatoração de um código já existente
  - `test`: teste unitário
  - `docs`: documentação

📚 Leia mais sobre Conventional Commits:

- 🔗 [Conventional Commits
](https://www.conventionalcommits.org/en/v1.0.0/)
