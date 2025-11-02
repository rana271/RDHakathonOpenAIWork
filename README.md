graph TD
    %% Define components
    A([Java Source Code]) as source
    B[Parser] as parser
    C([Intermediate Representation \(AST\)]) as ast
    D[AI/ML Model \(LLM\)] as llm
    E[TypeScript Code Generator] as generator
    F[Refactor & Quality Checker] as checker
    G([TypeScript Code]) as target

    %% Define the flow
    source --> B [ "Parse Code" ];
    B --> C [ "Generate AST" ];
    C --> D [ "Analyze & Translate" ];
    D --> E [ "Generate Code" ];
    E --> F [ "Review & Refine" ];
    F --> G [ "Final Output" ];

    %% Apply some styling for better visual clarity
    classDef io fill:#e0f7fa,stroke:#00acc1;
    class A,C,G io;
    classDef process fill:#b2ebf2,stroke:#0097a7;
    class B,D,E,F process;
# NewRepo
