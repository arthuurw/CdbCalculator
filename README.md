# Simulador de Investimento CDB

Aplicação full-stack que simula o retorno bruto e líquido de um investimento em CDB (Certificado de Depósito Bancário).

O backend expõe uma API HTTP que recebe o valor inicial do investimento e o prazo em meses e retorna os valores bruto e líquido. O frontend oferece uma interface moderna para interagir com a simulação.

A solução segue uma arquitetura em camadas que separa a camada HTTP, a lógica de aplicação e as regras de negócio.

---

# Estrutura do Projeto

```text
CdbCalculator
├─ backend
│  ├─ src
│  │  ├─ CdbCalculator.Api
│  │  ├─ CdbCalculator.Application
│  │  └─ CdbCalculator.Domain
│  └─ tests
│     └─ CdbCalculator.UnitTests
│
└─ frontend
   └─ cdbcalculator
      └─ src
         ├─ app
         │  ├─ core
         │  │  └─ services
         │  └─ features
         │     └─ cdb-simulation
         │        ├─ models
         │        └─ pages
         └─ environments
```

---

## Responsabilidades das Camadas

**CdbCalculator.Api**

Responsável por:

- endpoints HTTP
- roteamento de requisições
- configuração de middlewares
- documentação Swagger
- configuração de injeção de dependência
- configuração de CORS

**CdbCalculator.Application**

Responsável por:

- casos de uso da aplicação
- validação de entrada
- orquestração entre camadas

**CdbCalculator.Domain**

Responsável por:

- cálculos financeiros
- regras de negócio centrais

**CdbCalculator.UnitTests**

Responsável por:

- testes unitários das camadas de domínio e aplicação

---

# Tecnologias

Backend:

- .NET 8
- ASP.NET Core Minimal API
- Swagger / OpenAPI
- xUnit v3

Frontend:

- Angular 19
- TypeScript 5.7
- RxJS 7.8
- Karma + Jasmine

Qualidade de Código:

- .NET analyzers
- SonarLint

---

# Regras de Negócio

O sistema simula um investimento em CDB com base na fórmula fornecida no desafio.

## Fórmula

**VFₙ = VI × [1 + (CDI × TB)]ⁿ**

Onde:

| Símbolo | Descrição |
|---------|-----------|
| VFₙ | Valor final após n meses |
| VI | Valor inicial do investimento |
| CDI | Taxa CDI mensal |
| TB | Percentual do banco sobre o CDI |
| n | Número de meses |

Para este exercício são utilizados os seguintes valores fixos:

| Variável | Valor |
|----------|-------|
| CDI | 0,9% ao mês |
| TB | 108% |

O investimento cresce com **juros compostos mensais**, ou seja, o resultado de cada mês serve de base para o mês seguinte. A fórmula é aplicada iterativamente a cada mês sem arredondamentos intermediários para preservar a precisão máxima.

## Arredondamento

Todos os valores monetários são arredondados para 2 casas decimais utilizando `MidpointRounding.AwayFromZero` (padrão comercial brasileiro — ABNT NBR 5891). O arredondamento é aplicado apenas ao resultado final, nunca aos valores intermediários durante o cálculo de juros compostos.

---

# Regras de Imposto de Renda

O imposto de renda incide apenas sobre o **rendimento** do investimento (valor bruto menos valor inicial).

| Prazo do Investimento | Alíquota |
|-----------------------|----------|
| Até 6 meses | 22,5% |
| De 7 a 12 meses | 20% |
| De 13 a 24 meses | 17,5% |
| Acima de 24 meses | 15% |

Cálculo:

```
Rendimento   = ValorBruto - ValorInicial
Imposto      = arredondar(Rendimento × Alíquota, 2)
ValorLíquido = ValorBruto - Imposto
```

---

# Endpoint da API

## Criar Simulação CDB

`POST /api/investments/cdb/simulations`

Cria uma simulação de investimento em CDB com base no valor inicial e no prazo informados.

### Parâmetros da Requisição

| Campo | Tipo | Restrição |
|-------|------|-----------|
| initialAmount | decimal | Deve ser no mínimo R$0,01 |
| months | inteiro | Deve ser maior que 1 (mínimo 2 meses) |

### Exemplo de Requisição

```json
{
  "initialAmount": 1000.00,
  "months": 12
}
```

### Exemplo de Resposta

```json
{
  "grossAmount": 1123.08,
  "netAmount": 1098.46
}
```

| Campo | Descrição |
|-------|-----------|
| grossAmount | Valor total antes dos impostos |
| netAmount | Valor final após dedução do imposto |

---

# Tratamento de Erros

A API implementa um middleware de tratamento global de exceções.

Os erros são retornados no formato ProblemDetails definido pela RFC 7807.

Exemplo de resposta de erro:

```json
{
  "title": "Requisição inválida",
  "status": 400,
  "detail": "O valor inicial deve ser no mínimo R$0,01.",
  "instance": "/api/investments/cdb/simulations"
}
```

Respostas possíveis:

| Código de Status | Descrição |
|-----------------|-----------|
| 200 | Resultado da simulação |
| 400 | Erro de validação |
| 500 | Erro interno inesperado |

---

# Documentação da API

A documentação Swagger é gerada automaticamente e está disponível apenas no ambiente de **Desenvolvimento**.

Após iniciar a aplicação em modo Development, acesse:

```
https://localhost:7190/swagger
```

O Swagger disponibiliza:

- descrição dos endpoints
- schemas de requisição e resposta
- exemplos de payload
- códigos de resposta HTTP

---

# Executando a Aplicação

## Requisitos

| Ferramenta | Versão |
|------------|--------|
| .NET SDK | 8.0 ou superior |
| Node.js | 18.0 ou superior |
| Angular CLI | 19.x |
| Git | Qualquer versão recente |

Para verificar se as ferramentas estão instaladas corretamente:

```bash
dotnet --version   # deve exibir 8.x.x ou superior
node -v            # deve exibir v18.x.x ou superior
ng version         # deve exibir Angular CLI 19.x
```

---

## Clonar o Repositório

```bash
git clone <url-do-repositorio>
cd CdbCalculator
```

---

## Configuração Inicial (apenas na primeira vez)

O backend utiliza HTTPS no desenvolvimento. É necessário confiar no certificado de desenvolvimento do .NET para que o navegador e o frontend aceitem a conexão sem erros:

```bash
dotnet dev-certs https --trust
```

> Se aparecer uma janela pedindo confirmação, clique em **Sim**. Este passo é feito apenas uma vez por máquina.

---

## Ordem de Inicialização

**O backend deve ser iniciado antes do frontend.** O frontend consome a API do backend — se o backend não estiver em execução, as simulações retornarão erro de conexão.

### 1. Iniciar o Backend

Abra um terminal e execute:

```bash
dotnet run --project backend/src/CdbCalculator.Api
```

Aguarde até ver a mensagem indicando que a aplicação está ouvindo. A API estará disponível em:

```
https://localhost:7190
```

O Swagger estará disponível em:

```
https://localhost:7190/swagger
```

### 2. Iniciar o Frontend

Abra um **segundo terminal** e execute:

```bash
cd frontend/cdbcalculator
npm install
ng serve
```

A aplicação estará disponível em:

```
http://localhost:4200
```

Acesse `http://localhost:4200` no navegador para utilizar o simulador.

---

# Executando os Testes

## Backend

Execute a partir da raiz do repositório:

```bash
dotnet test backend/tests/CdbCalculator.UnitTests
```

Os testes validam:

- cálculo do valor bruto do CDB com juros compostos
- cálculo do imposto de renda em todas as faixas
- regras de validação de entrada
- orquestração do serviço de aplicação

## Frontend

```bash
cd frontend/cdbcalculator
ng test --watch=false
```

---

# Cobertura de Testes

## Backend — 37 testes unitários

| Camada | Testes |
|--------|--------|
| Domínio (GrossCdbCalculator) | 10 |
| Domínio (IncomeTaxCalculator) | 11 |
| Aplicação (CalculateCdbService) | 3 |
| Aplicação (CalculateCdbValidator) | 9 |
| Aplicação (DTOs) | 4 |

Cobertura (camadas lógicas):

| Métrica | Resultado |
|---------|-----------|
| Cobertura de Linhas | 97% |
| Cobertura de Branches | 93% |

## Frontend — 22 testes unitários

| Classe | Testes |
|--------|--------|
| AppComponent | 2 |
| CdbSimulationService | 4 |
| HomeComponent | 16 |

Cobertura frontend: **100%** (statements, branches, functions, lines).

---

# Qualidade de Código

A solução segue boas práticas comuns de desenvolvimento backend, incluindo:

- princípios SOLID
- arquitetura em camadas
- injeção de dependência
- tratamento centralizado de erros
- respostas HTTP padronizadas (RFC 7807)

Ferramentas de análise estática:

- .NET analyzers
- SonarLint

---

# Decisões de Design

## Arquitetura em Camadas

O sistema separa as responsabilidades HTTP, orquestração de aplicação e lógica de negócio em camadas independentes.

## Minimal API

A Minimal API foi utilizada para manter a camada HTTP concisa e objetiva.

## Standalone Components (Angular)

O frontend utiliza standalone components do Angular 19 com providers funcionais, eliminando a necessidade de NgModules.

## Injeção de Dependência

Os serviços de aplicação são registrados por meio de extension methods para manter a configuração de startup organizada. O `CalculateCdbService` é registrado como singleton pois não possui estado mutável.

## Tratamento Global de Exceções

Um middleware customizado centraliza o tratamento de erros e garante respostas HTTP consistentes em todos os endpoints.

## ProblemDetails

As respostas de erro seguem o formato ProblemDetails definido pela RFC 7807, garantindo um contrato de erro consistente e padronizado.

## Arredondamento AwayFromZero

Todo arredondamento monetário utiliza `MidpointRounding.AwayFromZero`, que corresponde ao padrão comercial brasileiro (ABNT NBR 5891) utilizado por instituições financeiras e pela Receita Federal.

---

# Autor

**Arthur Webster Moreira**
arthurwebster01@gmail.com
