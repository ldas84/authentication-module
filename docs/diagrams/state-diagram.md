stateDiagram-v2

    [*] --> Created

    Created --> Active: Token stored in DB

    Active --> Used: Token used for refresh
    Active --> Expired: Expiration reached
    Active --> Revoked: Manual revocation

    Used --> Replaced: Rotation generates new token

    Expired --> [*]
    Revoked --> [*]
    Replaced --> [*]