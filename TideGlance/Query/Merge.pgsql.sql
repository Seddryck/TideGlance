INSERT INTO $tableName$ (
	$identityFields:{field | $field.name$}; separator=", \r\n"$
	$valueFields:{field | $field.name$ = @$field.name$}; separator=", \r\n"$
)
VALUES (
	$fields:{field | @$field.name$}; separator=", \r\n"$
)
ON CONFLICT (
	$identityFields:{field | $field.name$}; separator=", \r\n"$
) DO UPDATE
SET 
	$valueFields:{field | $field.name$ = @$field.name$}; separator=", \r\n"$
;