# Simulador CDB — Frontend

Aplicação single-page em Angular 19 para simular o retorno de investimentos em CDB (Certificado de Depósito Bancário). Comunica-se com a [API backend](../../README.md) para calcular os valores bruto e líquido com base no valor inicial e no prazo de investimento.

> **Atenção:** o backend deve estar em execução antes de utilizar o simulador. Consulte o [README principal](../../README.md) para instruções completas de configuração e inicialização.

---

# Estrutura do Projeto

```text
src/
├─ app/
│  ├─ app.component.ts        # Componente raiz (router outlet)
│  ├─ app.config.ts           # Providers da aplicação (HttpClient, Router, locale pt-BR)
│  ├─ app.routes.ts           # Definição de rotas
│  ├─ core/
│  │  └─ services/
│  │     └─ cdb-simulation.service.ts   # Serviço HTTP
│  └─ features/
│     └─ cdb-simulation/
│        ├─ models/
│        │  ├─ cdb-simulation-request.ts
│        │  └─ cdb-simulation-response.ts
│        └─ pages/
│           └─ home/
│              ├─ home.component.ts
│              ├─ home.component.html
│              └─ home.component.css
├─ environments/
│  ├─ environments.ts                  # Ambiente de produção
│  └─ environments.development.ts      # Ambiente de desenvolvimento (localhost)
├─ index.html
├─ main.ts
└─ styles.css
```

---

# Tecnologias

| Tecnologia | Versão |
|------------|--------|
| Angular | 19.2 |
| TypeScript | 5.7 |
| RxJS | 7.8 |
| Karma | 6.4 |
| Jasmine | 5.6 |

---

# Requisitos

| Ferramenta | Versão |
|------------|--------|
| Node.js | 18.0 ou superior |
| npm | 9.0 ou superior |
| Angular CLI | 19.x |

Para verificar se as ferramentas estão instaladas:

```bash
node -v     # deve exibir v18.x.x ou superior
npm -v      # deve exibir 9.x.x ou superior
ng version  # deve exibir Angular CLI 19.x
```

Instale o Angular CLI globalmente caso ainda não esteja instalado:

```bash
npm install -g @angular/cli
```

---

# Iniciando o Projeto

### 1. Instalar dependências

Na pasta `frontend/cdbcalculator`, execute:

```bash
npm install
```

### 2. Iniciar o servidor de desenvolvimento

```bash
ng serve
```

A aplicação estará disponível em:

```
http://localhost:4200
```

Abra `http://localhost:4200` no navegador. Certifique-se de que o backend já está em execução em `https://localhost:7190`, caso contrário as simulações retornarão erro de conexão.

---

# Configuração de Ambiente

O endereço da API é definido nos arquivos de ambiente:

| Arquivo | Ambiente | `apiUrl` |
|---------|----------|----------|
| `environments.ts` | Produção | URL da API em produção (definir antes do deploy) |
| `environments.development.ts` | Desenvolvimento | `https://localhost:7190/api` |

Durante o desenvolvimento (`ng serve`), o Angular substitui automaticamente `environments.ts` por `environments.development.ts` via `fileReplacements` no `angular.json` — não é necessário alterar nenhum arquivo manualmente.

---

# Build

### Build de desenvolvimento

```bash
ng build --configuration development
```

### Build de produção

```bash
ng build
```

Os artefatos de build são gerados em `dist/cdbcalculator/`.

---

# Executando os Testes

Executar em modo watch (reexecuta ao salvar arquivos):

```bash
ng test
```

Executar uma única vez sem modo watch:

```bash
ng test --watch=false
```

Executar com relatório de cobertura:

```bash
ng test --watch=false --code-coverage
```

O relatório de cobertura é gerado em `coverage/cdbcalculator/index.html`.

---

# Cobertura de Testes

**100%** em todas as métricas.

| Métrica | Cobertura |
|---------|-----------|
| Statements | 100% (48/48) |
| Branches | 100% (8/8) |
| Functions | 100% (5/5) |
| Lines | 100% (45/45) |

## Arquivos de teste

| Arquivo | Testes | Cobre |
|---------|--------|-------|
| `app.component.spec.ts` | 2 | Criação do componente, renderização do router-outlet |
| `cdb-simulation.service.spec.ts` | 4 | URL do POST HTTP, corpo da requisição, mapeamento da resposta |
| `home.component.spec.ts` | 16 | Validação de entradas, fluxo de sucesso, tratamento de erros, reset |

---

# Decisões de Arquitetura

## Standalone Components

Todos os componentes utilizam a API standalone do Angular 19 — sem NgModules. Os providers são configurados funcionalmente em `app.config.ts`.

## Injeção de Dependência Funcional

As dependências são injetadas com a função `inject()` e `DestroyRef` para limpeza vinculada ao ciclo de vida, dispensando injeção via construtor.

## Prevenção de Memory Leaks

As assinaturas HTTP no `HomeComponent` utilizam `takeUntilDestroyed(destroyRef)` para cancelar automaticamente requisições em andamento quando o componente é destruído.

## Locale pt-BR

A aplicação registra o locale `pt-BR` e define o `LOCALE_ID` globalmente, garantindo que o pipe de moeda do Angular formate valores em BRL corretamente para usuários brasileiros (ex.: `R$ 1.060,03` em vez de `R$1,060.03`).

## Substituição de Ambiente

O serviço importa de `environments.ts` (produção). A configuração `fileReplacements` do Angular substitui esse arquivo por `environments.development.ts` em tempo de build na configuração de desenvolvimento — sem referências de ambiente hardcoded nos arquivos de código-fonte.

---

# Autor

**Arthur Webster Moreira**
arthurwebster01@gmail.com
