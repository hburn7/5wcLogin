# 5WC API

### Base URL: `https://auth.stagec.xyz/api`


### Endpoints:

```
/osu
```

**Parameters:**
* `k` - API Key (required)
* `osuid` - (required) osu! ID of the user to retrieve

**Returns:**
* If the user has registered (sample user):
```
{
  "Id": 5,
  "OsuID": "12345",
  "OsuDisplayName": "Test",
  "DiscordID": "987654321",
  "DiscordDisplayName": "Test#0000",
  "RegistrationDate": "2022-12-15T23:12:16.145Z",
  "OsuJson": "{...}"
  ""
}
```
* If the user has not registered:
```
{}
```

```
/osu/all
```

**Parameters:**
* `k` - API Key (required)

**Returns:**
* A list of all registered users per the format for `/osu`, otherwise `{}`