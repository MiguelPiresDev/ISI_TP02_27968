# Trabalho Prático II
Integração de Sistemas de Informação 

Licenciatura em Engenharia de Sistemas Informáticos (regime *laboral*) 2025-26

## constituição do grupo
| número  | nome         | email       |
| ------  | ------------ | ----------  |
| 27968   | Miguel Pires | a27968@alunos.ipca.pt |



## descrição do problema a resolver 
  
tema
: Imobiliário (SmartEstate)  

breve descrição
: Consiste no desenvolvimento de uma plataforma de integração de sistemas para gestão imobiliária, visando resolver a fragmentação de dados, a ineficiência no agendamento de visitas e a falta de contexto geográfico dos ativos.

mais informação
: 
- Integração de serviços externos: **OpenStreetMap** (Geocoding) e **Exchange Rates API** (Câmbios).
- Interoperabilidade temporal: Exportação de agendamentos em formato standard **.ics** (iCalendar).
- Arquitetura orientada a serviços (SOA) híbrida, combinando **SOAP** (Dados) e **REST** (Lógica/Microserviços).


para uma descrição do problema e arquitetura prevista para a solução consultar: 
[doc_27968-descricao.pdf](./doc/doc_27968-descricao.pdf)



## organização do repositório

[doc/](./doc/)  documentação com o relatório


[src/](./src/)  código da solução desenvolvida  

* **SmartEstate.DataLayer (SOAP):** Serviço WCF/ASMX responsável pela camada de acesso a dados e transações com a base de dados SQL Server.
* **SmartEstate.Auth (REST):** Microserviço independente para gestão de identidade e emissão de tokens JWT.
* **SmartEstate.API (REST):** API Core de negócio que orquestra a gestão de imóveis, consome as APIs externas (Mapas/Câmbios) e gera os ficheiros .ics.
* **SmartEstate.Web (MVC):** Aplicação Cliente Web (Frontend) para visualização do Dashboard, Mapas e gestão administrativa.
