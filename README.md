# paste.solutions

I believe that it should be simple to share code snippets, and that's what I hope to accomplish with this service.

## Usage

New pastes can be created at https://paste.solutions/.
They can be saved by either pressing the 'save' icon, or by pressing Ctrl + S.

There's also a rest endpoint at https://paste.solutions/api/upload that accepts json in the following format:

	```json
	{
	  "snippet": "<the paste>",
	  "language": "<the language of the paste>"
	}
	```

which will generate the following response:

	```json
	{
	  "id": "<id of the saved snippet>"
	}
	```

or an error:

	```json
	{
	  "error": "<error message>",
	  "error_code": "<error code>"
	}
	```

## Viewing snippets

The format of the endpoint is https://paste.solutions/{id}.{extension}.
The Extension can be omitted or replaced when highlight js doesn't determine the right language.

If you need to view the raw content of the paste, simply append **/raw** to the url. 


## Lifetime of snippets

Currently there is no lifetime limit, but in future I'll be pruning all snippets older than 10 days.

## License

This project is FOSS software licensed under LGPL 2.0