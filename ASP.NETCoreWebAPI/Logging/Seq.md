# Seq

Seq can run on other computer just for logging analysis
Seq can be found at the address specified in the appsettings.json

To filter results in the seq we can write:
Method = "GET"
we can click on @level -> Errors, Warnings

We can use statistics using SQL query:
select count(*) from stream group by time(1h)

U can click on the calendar to choose the logs from day or click "Now" to clean the table and prepare it for upcoming logs

We can filter by certain metadata:
UserId = "someone"

Important thing to examine the logs of RequestId -> each request has the unique RequestId. We can filter by RequestId and see everything what happened during this id
RequestId = "0HMGVG1VL51GU:00000017"

Other filtering 
id = 43

```json
{
    "Name": "Seq",
    "Args": { "serverUrl": "http://localhost:5341" } //use seq for logging (username: admin, password: serilog123SEQ)
}
```