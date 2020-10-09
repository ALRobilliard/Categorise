# Categorise API

![Continuous Integration](https://github.com/ALRobilliard/Categorise/workflows/Continuous%20Integration/badge.svg)
![Continuous Deployment](https://github.com/ALRobilliard/Categorise/workflows/Continuous%20Deployment/badge.svg)

This is a REST API providing access to a PostgreSQL database containing monetary transaction data.

Once complete, the API will provide functionality for tracking personal spending, with the ability to:
- Bulk upload transactions from CSV files
- Categorise individual transactions on upload
- Create and maintain a list of transaction mappings, for automatic categorisation
- Re-extract stored transactions
- Add tags to transactions

## Call Structure

### GET all

https://localhost:5002/api/**[controller]**

### GET single

https://localhost:5002/api/**[controller]**/00000000-0000-0000-0000-000000000000

### POST an item

https://localhost:5002/api/**[controller]**

Category Example:
```json
{
  "CategoryName": "Groceries"
}
```

### PUT an item

https://localhost:5002/api/**[controller]**/00000000-0000-0000-0000-000000000000

Category Example:
```json
{
  "Id": "00000000-0000-0000-0000-000000000000",
  "CategoryName": "Food"
}
```

### DELETE an item

https://localhost:5002/api/**[controller]**/00000000-0000-0000-0000-000000000000