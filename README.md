# UnitTest_MVC

This repository contains an example project demonstrating unit testing in an MVC (Model-View-Controller) architecture.

## Table of Contents

- [Introduction](#introduction)
- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
- [Running Tests](#running-tests)

## Introduction

The purpose of this project is to showcase how to write unit tests for an MVC application. It includes examples of testing controllers and services.

## Technologies Used

- **Framework**: ASP.NET Core MVC, NUnit.Framework
- **Testing Framework**: NUnit - [Docs](https://docs.nunit.org/index.html)
- **Mocking Library**: Moq
- **VSCode Extension**: [Test Adapter Converter](https://marketplace.visualstudio.com/items?itemName=ms-vscode.test-adapter-converter) for test coverage.

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/dhminh01/UnitTest_MVC.git
   ```
2. Navigate to the project directory:
   ```bash
   cd UnitTest_MVC
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```

## Running Tests

To run the unit tests, execute the following command:

```bash
cd MVC_Assignment_2.Tests
dotnet build
dotnet test
```
