## Sobre o projeto
Esta API, desenvolvida utilizando .NET 8, adota os princípios do **Domain-Driven Design (DDD)*** para oferecer uma solução estruturada e eficaz no gerenciamento de despesas pessoais. O principal objetivo é permitir que os usuários registrem suas despesas, detalhando informações como título, data e hora, descrição, valor e tipo de pagamento, com os dados sendo armazenados de forma segura em um banco de dados MySQL.

A arquitetura da API baseia-se em ***REST***, utilizando métodos ***HTTP*** padrão para uma comunicação eficiente e simplificada. Além disso, é complementada por uma documentação ***Swagger***, que proporciona uma interface gráfica interativa para que os desenvolvedores possam explorar e testar os endpoints de maneira fácil.

Dentre os pacotes NuGet utilizados:
- ***AutoMapper*** é responsável pelo mapeamento entre objetos de domínio e requisição/resposta, reduzindo a necessidade de código repetido e manual.
- ***FluentAssertions*** é utilizado nos testes de unidade para tornar as verificações mais legíveis, ajudando a escrever testes claros e compreensíveis.
- ***FluentValidation*** é usado para implementar regras de validação de forma simples e intuitiva nas classes de requisições, mantendo o código limpo e fácil de manter.
- ***EntityFramework*** atua como um ORM (Object-Relational Mapper) que simplifica as interações com o banco de dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessidade de lidar com consultas SQL.

### Features
---

- **Domain-Driven Design (DDD)**:  
  A arquitetura do projeto foi estruturada com base nos conceitos de DDD, promovendo uma separação clara entre camadas (Domínio, Aplicação, Infraestrutura e Apresentação) e garantindo que as regras de negócio permaneçam isoladas e bem definidas.

- **Testes de Unidade**:  
  Implementação de testes unitários utilizando o framework xUnit junto com FluentAssertions, proporcionando validações automatizadas de métodos e classes críticas, assegurando a qualidade e robustez do código.

- **Geração de Relatórios (PDF e Excel)**:  
  Implementação de serviços responsáveis pela geração de relatórios dinâmicos em formatos PDF e Excel, possibilitando exportação dos dados registrados na API de maneira prática e automatizada.

- **API RESTful**:  
  Exposição de endpoints seguindo os princípios REST, utilizando os métodos HTTP (GET, POST, PUT, DELETE) e padrões de boas práticas, garantindo uma interface de comunicação padronizada e de fácil integração com clientes front-end ou sistemas externos.



### Requisitos
---
Antes de rodar o projeto, é necessário ter instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)  
  Ambiente de desenvolvimento para compilar e executar o projeto.

- [MySQL Server 8.0+](https://dev.mysql.com/downloads/mysql/)  
  Banco de dados relacional utilizado para armazenar as informações da aplicação.

- [Visual Studio 2022 ou superior](https://visualstudio.microsoft.com/) (ou qualquer IDE compatível com .NET)  
  Recomendado para desenvolvimento e execução da aplicação com suporte a ferramentas como Entity Framework e Swagger.

- [Postman](https://www.postman.com/) (opcional)  
  Para testar manualmente os endpoints da API de forma prática.

- [Git](https://git-scm.com/) (opcional)  
  Para clonar o repositório e gerenciar o versionamento do projeto.


### Instalação
---

Clone o repositório:

- git clone https://github.com/joaobernardo1/CashFlow.git

- Abra o projeto no Visual Studio 2022+ ou Visual Studio Code.

- Configure a conexão com o banco de dados:

- Utilize o Swagger ou Postman para testar todos os endpoints disponíveis.
