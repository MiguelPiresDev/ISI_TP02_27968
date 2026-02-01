# 🏠 Plataforma de Gestão Imobiliária - Cloud Solution

Este projeto consiste numa solução completa de gestão imobiliária, desenvolvida com uma arquitetura distribuída e **totalmente alojada na nuvem (Microsoft Azure)**.

O sistema é composto por uma Web API (Backend) que comunica com uma Base de Dados SQL Azure, e uma Interface Web (Frontend) para interação do utilizador.

---

## 🚀 Como Executar o Projeto

**Não é necessária qualquer instalação local.** O projeto encontra-se publicado e ativo.

Para aceder e testar a solução, utilize os seguintes links:

### 🌐 1. Aplicação Web (Frontend)
Aceda à interface gráfica para gerir propriedades, clientes e vendas:
> **https://imobiliario-web-pires-drf2ayamdvfea4bg.francecentral-01.azurewebsites.net**

### ⚙️ 2. Web API (Backend)
O serviço RESTful está disponível no seguinte endereço:
> **https://imobiliario-api-pires-hmh8ebhbhwf7b4eh.francecentral-01.azurewebsites.net**

---

## 🛠️ Tecnologias Utilizadas

* **Backend:** C# .NET Web API
* **Base de Dados:** Azure SQL Database
* **Hosting:** Azure App Service
* **Testes de Integração:** Postman

---

## 🧪 Testes de API (Postman)

A API foi desenhada para suportar operações CRUD completas. Exemplos de endpoints funcionais:

* **POST** `/api/Register` - Registo de novos utilizadores.
* **POST** `/api/Login` - Autenticação.
* **GET** `/api/Property` - Listagem de propriedades.
* **POST** `/api/Sales` - Registo de venda (com validação automática de datas e proprietários).

> **Nota:** A API processa automaticamente as regras de negócio, como a atribuição da data atual na venda e a verificação de chaves estrangeiras (Foreign Keys).


**Desenvolvido por:** Miguel Pires
**Unidade Curricular:** Integração de Sistemas Informáticos
**Data:** 28 Dezembro 2025
