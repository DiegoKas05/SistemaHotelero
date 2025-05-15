
# Como hacer un commit:

Normas detalladas:
1. Verbos Imperativos:
Usa verbos en imperativo para describir la acci�n del commit. Por ejemplo, en lugar de "Agregada nueva caracter�stica", escribe "A�adir nueva caracter�stica". 

2. Sin Punto Final:
El mensaje no debe terminar con un punto. El mensaje debe ser conciso y directo, como un t�tulo de secci�n. 

3. Longitud del Mensaje:
Limita el mensaje a 50 caracteres o menos. Si necesitas m�s informaci�n, utiliza el cuerpo del commit. 

4. May�sculas:
No uses may�sculas en el mensaje. El mensaje debe ser en min�sculas. 
5. Cuerpo del Commit:
Usa el cuerpo del commit para explicar el "qu�" y el "por qu�" de los cambios, no el "c�mo". 

6. Separa el Mensaje del Cuerpo:
Deja una l�nea en blanco entre el t�tulo del commit y el cuerpo del commit. 

7. L�nea en Blanco:
Dejar una l�nea en blanco entre el t�tulo y el cuerpo del commit ayuda a la lectura y comprensi�n del historial de commits. 

8. Uso de Prefijos:
Utiliza prefijos sem�nticos como feat, fix, docs, etc., para clasificar el tipo de commit, de acuerdo con Midudev.

9. Herramientas de Validacion:
Considera usar herramientas como Husky o Commitlint para validar la coherencia de tus mensajes de commit. 

10. Ejemplos:

feat: A�adir nueva funci�n de b�squeda
fix: Corregir error de visualizaci�n
docs: Actualizar documentaci�n. 

El cuerpo del commit:
El cuerpo del commit debe ser una descripci�n m�s detallada de los cambios. Debe explicar el "qu�" y el "por qu�" de los cambios, no el "c�mo". Tambi�n puede incluir informaci�n adicional, como referencias a issues o pull requests. 


Esta funci�n permite a los usuarios buscar contenido en la base de datos.
Se ha a�adido una nueva p�gina de b�squeda y se ha implementado una nueva API.
Beneficios de usar mensajes imperativos:

Claridad:
Los mensajes imperativos son m�s claros y concisos, lo que facilita la comprensi�n de los cambios realizados.

Estandarizaci�n:
El uso de mensajes imperativos ayuda a estandarizar el historial de commits.

Colaboraci�n:
Permite una mejor colaboraci�n entre desarrolladores, ya que los mensajes son f�ciles de entender. 

Documentaci�n:
Contribuyen a la documentaci�n del c�digo, haciendo m�s f�cil entender la evoluci�n del proyecto. 




# Como crear un issues

Pasos detallados:
1. Navegar al repositorio: Ve a la p�gina principal del repositorio de GitHub donde quieres crear el "issue". 

2.Acceder a la secci�n de "Issues": Debajo del nombre del repositorio, haz clic en "Issues".

3.Crear un nuevo "issue": Haz clic en "New issue" (Nueva propuesta). 

4. Elegir plantilla o crear en blanco: Si el repositorio tiene plantillas de "issue", puedes usar una de ellas haciendo clic en "Start" (Comenzar) junto a la plantilla deseada. Si no, puedes crear un "issue" en blanco haciendo clic en "Open a blank issue". 

5. Llenar la informaci�n:

	-T�tulo: Escribe un t�tulo claro y conciso que describa el problema. 
	-Descripci�n: Describe el problema con detalle en el cuerpo del "issue". Incluye pasos para reproducirlo, capturas de pantalla o cualquier informaci�n relevante. 

6. Opcional: Asignar, agregar a proyecto, hito, etiqueta: Si eres mantenedor del proyecto, puedes asignar el "issue" a alguien, agregar al proyecto, asociar a un hito, establecer el tipo de incidencia o aplicar una etiqueta. 

7.Enviar: Haz clic en "Submit new issue" (Enviar nueva propuesta). 

Ejemplos de "issues" comunes:
	-Reporte de errores:
"El bot�n de env�o no funciona en la p�gina principal." (Detalles: "Al hacer clic en el bot�n, no ocurre nada. He probado a actualizar la p�gina y limpiar la cach�, pero el problema persiste.").
Petici�n de nueva funcionalidad:
"A�adir la opci�n de exportar datos a formato CSV." (Detalles: "Esto facilitar�a el an�lisis de datos fuera de la plataforma.").