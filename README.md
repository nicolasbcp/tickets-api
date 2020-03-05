# Tickets API Guide

Este é um guia simplificado para utilização da ferramenta.

A inicialização da mesma poderá ser feita através de qualquer terminal.

Será necessária atualização da Connection String.

## Restore packages

    dotnet restore

## Update database

    dotnet ef database update

## Run the application

    dotnet watch run

# REST API

Os exemplos para usá-la serão disponibilizados a seguir.

## Método Get

### Requisição

`GET /eventos/`

    'Accept: application/json' http://localhost:5001/api/v1/eventos

### Resposta

      "id": 1
      "nome": "New Order - Show"
      "capacidade": 1000
      "data": "2020-04-03T00:00:00"
      "valorUnitario": 750
      "casaDeShowID": 1
      "casaDeShow": "Live Curitiba - Masterhall"
      "generoMusicalID": 1
      "generoMusical": "Rock"
      
    []

## Método Post

### Requisição

`POST /eventos/`

    'Accept: application/json' http://localhost:5001/api/v1/eventos
    
      "id": 1
      "nome": "Pink Floyd - Show"
      "capacidade": 5000
      "data": "2020-03-05T17:24:05.496Z"
      "valorUnitario": 850.00
      "casaDeShowID": 1
      "generoMusicalID": 1
      
    []

### Resposta

      "mensagem": "Evento cadastrado com sucesso!"
      "dado": {
        "id": 1
        "nome": "Pink Floyd - Show"
        "capacidade": 5000
        "data": "2020-03-05T17:24:05.496Z"
        "valorUnitario": 850
        "casaDeShowID": 1
        "generoMusicalID": 1
        
      []

## Método Get por Id

### Requisição

`GET /eventos/id`

    'Accept: application/json' http://localhost:5001/api/v1/eventos/{id}

### Resposta

      "mensagem": "Evento encontrado com sucesso!",
      "dado": {
        "id": 1,
        "nome": "New Order - Show",
        "capacidade": 1000,
        "data": "2020-04-03T00:00:00",
        "valorUnitario": 750,
        "casaDeShowID": 1,
        "casaDeShow": "Live Curitiba - Masterhall",
        "generoMusicalID": 1,
        "generoMusical": "Rock"
        
      []

## Método Get por Id (inexistente)

### Requisição

`GET /eventos/id`

    application/json' http://localhost:5001/api/v1/eventos/{id}

### Resposta

        HTTPS 404 Not Found
        Status: 404 Not Found
        "mensagem": "Evento não encontrado!",
        "dado": null
        
      []

## Métodos Put

### Requisição

`PUT /eventos/id`

    'Accept: application/json' http://localhost:5001/api/v1/eventos/{id}
    
      "id": 1,
      "nome": "New Order - Show (changed)",
      "capacidade": 1001,
      "data": "2020-03-05T17:37:40.126Z",
      "valorUnitario": 751,
      "casaDeShowID": 1,
      "generoMusicalID": 1
      
    []

### Resposta

      "mensagem": "Evento editado com sucesso!",
      "dado": {
        "id": 1,
        "nome": "New Order - Show (changed)",
        "capacidade": 1001,
        "data": "2020-03-05T17:37:40.126Z",
        "valorUnitario": 751,
        "casaDeShowID": 1,
        "generoMusicalID": 1
        
      []

## Método Put com parâmetros inexistentes

### Requisição

`PUT /eventos/id`

    application/json' http://localhost:5001/api/v1/eventos/{id}
    
        "id": 1000,
        "nome": "New Order - Show (changed)",
        "capacidade": 1001,
        "data": "2020-03-05T17:37:40.126Z",
        "valorUnitario": 751,
        "casaDeShowID": 1,
        "generoMusicalID": 1
      
      []

### Resposta

        HTTPS 404 Not Found
        Status: 404 Not Found
        "mensagem": "Erro ao editar casa de show.",
        "dado": [
          "Casa de Show inexistente. "
        
      []

## Método Delete

### Requisição

`DELETE /eventos/id`

    'Accept: application/json' http://localhost:5001/eventos/{id}

### Response

        "mensagem": "Evento excluído com sucesso!",
        "dado": {
          "id": 1,
          "nome": "New Order - Show (changed)",
          "capacidade": 1001,
          "data": "2020-03-05T17:37:40.126",
          "valorUnitario": 751,
          "casaDeShow": {
            "id": 1,
            "nome": "Live Curitiba - Masterhall",
            "endereco": "R. Itajubá, n. 200"
          },
          "casaDeShowID": 1,
          "generoMusical": {
            "id": 1,
            "nome": "Rock"
          },
          "generoMusicalID": 1
          
        []

## Método Delete com Id inexistente

### Requisição

`DELETE /thing/id`

    'Accept: application/json' http://localhost:5001/eventos/{id}

### Resposta

          HTTPS 404 Not Found
          Status: 404 Not Found
          "mensagem": "Evento inexistente.",
          "dado": null
            
        []
