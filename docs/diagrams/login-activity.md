flowchart TD

    A([Start]) --> B[Get email and password]
    B --> C[Validate input data]

    C -->|Invalid Data| Z[Error 400 - Bad Request]
    C -->|Valid Data| D[Search user by email]

    D -->|Not exist| E[Error 401 - Unauthorized]
    D -->|Exist| F[Verify Password]

    F -->|Incorrect| E2[Error 401 - Unauthorized]
    F -->|Correct| G[Generate Access Token]

    G --> H[Generate Refresh Token]
    H --> I[Store Refresh Token en DB]

    I --> J[Return tokens (200 OK)]
    J --> K([End])