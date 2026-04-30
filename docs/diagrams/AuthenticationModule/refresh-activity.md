flowchart TD

    A([Start]) --> B[Get refresh token]
    B --> C[Validate token is present]

    C -->|Token not found| Z[Error 400 - Bad Request]
    C -->|Token found| D[Search for the token into database]

    D -->|Not exist| E[Error 401 - Unauthorized]
    D -->|Exist| F[Validate token status]

    F -->|Expired o revocated| E2[Error 401 - Unauthorized]
    F -->|Active| G[Generate Access Token]

    G --> H[Generate new Refresh Token (rotation)]
    H --> I[Store the new Refresh Token in DB]
    I --> J[Mark previous token as revocated/replaced]

    J --> K[Return tokens (200 OK)]
    K --> L([End])