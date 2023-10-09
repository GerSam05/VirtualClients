<p align="center">
  <img width="626" height="134" src="https://github.com/GerSam05/GalponIndustrial/assets/146037370/6623ae64-08b3-46f2-b04b-74edfbade9e8"><br>
  <img src="https://komarev.com/ghpvc/?username=gersam05&label=Profile%20views&color=0e75b6&style=flat" alt="gersam05" />
</p>


# 📲API Restfull VirtualClients 

## Introducción
-	**VirtualClients** es una **API Restfull**  diseñada como una herramienta para la empresa prestadora de servicios comerciales **Centro de Copiado E9G** con el objeto de realizar operaciones **CRUD** en una base de datos remota compuesta por dos tablas relacionadas (imgs:1 y 2).
-	La Api posee un único controlador **ClientController**, para realizar consultas en la tabla Cliente.
-	La metodología utilizada fué **CodeFirst** con el **ORM** de .**NET EntityFrameworkCore**, todas las operaciones CRUD se efectúan a través de **Stored Procedures** (Procedimientos Almacenados) (imgs:3 y 4), creados a partir de **migraciones**.
-	La clase “**AppDbContext**” heredera del **DbContext** se sobrescribió con dos objetivos: mantener los nombres de las tablas en la base de datos en singular y establecer una clase especial “**ClienteTotal**” que permita ejecutar un query de tipo **inner join** en ambas tablas a través de un stored procedure con el método **GetTotal()** del ClientController.
-	El modelo está basado en los patrones de diseño **Dependency Injection** y **Service Layer**, con una clase "capa" entre el contexto y el controlador, además se agregaron Dtos de entrada para flexibilizar la validación de los Json en los endpoints.
-	La Api posee una clase "**APIResponse**" con métodos, encargada de retornar una respuesta estandarizada para todas las peticiones realizadas desde los endpoints.


<br>

## Tecnologías utilizadas

<p align="left"> <a href="https://www.w3schools.com/cs/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="40" height="40"/> </a> <a href="https://dotnet.microsoft.com/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dot-net/dot-net-original-wordmark.svg" alt="dotnet" width="40" height="40"/> </a> <a href="https://git-scm.com/" target="_blank" rel="noreferrer"> <img src="https://www.vectorlogo.zone/logos/git-scm/git-scm-icon.svg" alt="git" width="40" height="40"/> </a> <a href="https://www.microsoft.com/en-us/sql-server" target="_blank" rel="noreferrer"> <img src="https://www.svgrepo.com/show/303229/microsoft-sql-server-logo.svg" alt="mssql" width="40" height="40"/> </a> <a href="https://postman.com" target="_blank" rel="noreferrer"> <img src="https://www.vectorlogo.zone/logos/getpostman/getpostman-icon.svg" alt="postman" width="40" height="40"/> </a> </p>
<br>

## Imágenes

Tablas de la base de datos:

![relationship](https://github.com/GerSam05/VirtualClients/assets/146037370/f5f15ee9-c0b8-43c2-927f-3b7e6aaeaa94)
> Imagen 1: relación entre las tablas
<br>

![Sin título](https://github.com/GerSam05/VirtualClients/assets/146037370/9873d9a2-36c4-46e1-b232-b788851965a9)
> Imagen 2: query "select * from" en ambas tablas.
<br>

Stored procedures:

![sp1](https://github.com/GerSam05/VirtualClients/assets/146037370/49ed152c-725f-4e8e-8b9f-61b29c0aed66)
> Imagen 3: Stored procedures.
<br>

![SP2](https://github.com/GerSam05/VirtualClients/assets/146037370/fb308439-6665-4e12-b8d4-19f7a33b3887)
> Imagen 4: Stored Procedures 2.
<br>

---

Espero que el repositorio les sea de utilidad👍🏻💡!!!...
 
> 📁 Todos mis proyectos estan disponibles en [![GitHub repository](https://img.shields.io/badge/repository-github-orange)](https://github.com/GerSam05?tab=repositories)
