# Why authorisation header is account+api key, need to replace with just api key
## Authorisation header is not forment properly (i.e. Bearer, or Basic),
## why base 64???

# reseller-id, software-house-id incorrect casing
## are they mandatory or not api ref and docs are showing different things

# API is not cloud friendly, it will have reliability issues due to single instance connection

# why session id is validated as UUID it should come from POS system, they may have different format

# createdAt is not UTC why? how are we using it on our side?

# why getTable when occupied does not return sessionId :slightly_smiling_face: ?
# relationship between table and session is not clear
# names: why do we have GetFullBill vs GetBillItems

# GetTable is inconsistent naming, it has tableName as 'name', in there places it is called tableName or tableNames

# ResellerId is optional, and software house id is mandatory
