# Clientes API

Este é um projeto Back-And de Cadastro de Usuarios. Essa API permite Criar, Deletar, Ler e Atualizar um Cliente no banco de dados.

## Requirements

- [.NET 8.0](https://dotnet.microsoft.com/en-us/download)
- [MySQL](https://dev.mysql.com/downloads/workbench/)
- [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/)


## Technologies

- C#
- .Net
- MySQL
- Swagger (Documentação)

## Métodos
Requisições para a API devem seguir os padrões:
| Método | Descrição |
|---|---|
| `GET` | Retorna informações de um ou mais registros. |
| `POST` | Utilizado para criar um novo registro. |
| `PUT` | Atualiza dados de um registro ou altera sua situação. |
| `DELETE` | Remove um registro do sistema. |



# Documentação da API

## Clientes [/api/Clientes]

api-version: 1.0 Default


### [ GET ] /api/Clientes


| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Cliente` | `Cliente` | Lista todos Clientes. |


+ Response 200 (application/json)

    + Body

             {
                "clienteId": 0,
                "nome": "string",
                "cpf": "stringstrin",
                "email": "user@example.com"
              }         




### [ GET ] /api/Clientes/{id}


| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Id` | `int` | Busca Clientes com parametro Id. |


+ Response 200 (application/json)

    + Body

             {
                "clienteId": 0,
                "nome": "string",
                "cpf": "stringstrin",
                "email": "user@example.com"
              }        

### [ GET ] /api/Clientes/clientes/{nome}


| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Nome` | `string` | Busca Clientes com parametro Nome. |


+ Response 200 (application/json)

    + Body

             {
                "clienteId": 0,
                "nome": "string",
                "cpf": "stringstrin",
                "email": "user@example.com"
              }      

### [ POST ] /api/Clientes/


| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Cliente` | `Cliente` | Busca Clientes com parametro Nome. |


+ Response 201 (application/json)

    + Body

             {
                "clienteId": 0,
                "nome": "string",
                "cpf": "stringstrin",
                "email": "user@example.com"
              }      

### [ PUT ] /api/Clientes/{id}


| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Id` | `int` | Atualiza Cliente de parametro Id. |


+ Response 200 (application/json)

    + Body

             {
                "clienteId": 0,
                "nome": "string",
                "cpf": "stringstrin",
                "email": "user@example.com"
              }      

### [ DELETE ] /api/Clientes/{id}


| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Id` | `int` | Deleta Cliente de parametro Id. |


+ Response 200 (application/json)

    + Body

             {
                "clienteId": 0,
                "nome": "string",
                "cpf": "stringstrin",
                "email": "user@example.com"
              }      
