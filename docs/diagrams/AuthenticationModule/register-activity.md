flowchart TD

    A([Start]) --> B[Get email and password]
    B --> C[Validate input data]

    C -->|Invalid Data| Z[Error 400 - Bad Request]
    C -->|Valid Data| D[Search user by email]

    D -->|Exist| E[Error 409 - Conflict]
    D -->|Not exist| F[Hash password]

    F --> G[Create user entity]
    G --> H[Store user into DB]

    H --> I[Return 201 Created]
    I --> J([End])