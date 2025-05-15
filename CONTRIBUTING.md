
# Como hacer un commit:

Normas detalladas:
1. Verbos Imperativos:
Usa verbos en imperativo para describir la acción del commit. Por ejemplo, en lugar de "Agregada nueva característica", escribe "Añadir nueva característica". 

2. Sin Punto Final:
El mensaje no debe terminar con un punto. El mensaje debe ser conciso y directo, como un título de sección. 

3. Longitud del Mensaje:
Limita el mensaje a 50 caracteres o menos. Si necesitas más información, utiliza el cuerpo del commit. 

4. Mayúsculas:
No uses mayúsculas en el mensaje. El mensaje debe ser en minúsculas. 
5. Cuerpo del Commit:
Usa el cuerpo del commit para explicar el "qué" y el "por qué" de los cambios, no el "cómo". 

6. Separa el Mensaje del Cuerpo:
Deja una línea en blanco entre el título del commit y el cuerpo del commit. 

7. Línea en Blanco:
Dejar una línea en blanco entre el título y el cuerpo del commit ayuda a la lectura y comprensión del historial de commits. 

8. Uso de Prefijos:
Utiliza prefijos semánticos como feat, fix, docs, etc., para clasificar el tipo de commit, de acuerdo con Midudev.

9. Herramientas de Validacion:
Considera usar herramientas como Husky o Commitlint para validar la coherencia de tus mensajes de commit. 

10. Ejemplos:

feat: Añadir nueva función de búsqueda
fix: Corregir error de visualización
docs: Actualizar documentación. 

El cuerpo del commit:
El cuerpo del commit debe ser una descripción más detallada de los cambios. Debe explicar el "qué" y el "por qué" de los cambios, no el "cómo". También puede incluir información adicional, como referencias a issues o pull requests. 


Esta función permite a los usuarios buscar contenido en la base de datos.
Se ha añadido una nueva página de búsqueda y se ha implementado una nueva API.
Beneficios de usar mensajes imperativos:

Claridad:
Los mensajes imperativos son más claros y concisos, lo que facilita la comprensión de los cambios realizados.

Estandarización:
El uso de mensajes imperativos ayuda a estandarizar el historial de commits.

Colaboración:
Permite una mejor colaboración entre desarrolladores, ya que los mensajes son fáciles de entender. 

Documentación:
Contribuyen a la documentación del código, haciendo más fácil entender la evolución del proyecto. 




# Como crear un issues

Pasos detallados:
1. Navegar al repositorio: Ve a la página principal del repositorio de GitHub donde quieres crear el "issue". 

2.Acceder a la sección de "Issues": Debajo del nombre del repositorio, haz clic en "Issues".

3.Crear un nuevo "issue": Haz clic en "New issue" (Nueva propuesta). 

4. Elegir plantilla o crear en blanco: Si el repositorio tiene plantillas de "issue", puedes usar una de ellas haciendo clic en "Start" (Comenzar) junto a la plantilla deseada. Si no, puedes crear un "issue" en blanco haciendo clic en "Open a blank issue". 

5. Llenar la información:

	-Título: Escribe un título claro y conciso que describa el problema. 
	-Descripción: Describe el problema con detalle en el cuerpo del "issue". Incluye pasos para reproducirlo, capturas de pantalla o cualquier información relevante. 

6. Opcional: Asignar, agregar a proyecto, hito, etiqueta: Si eres mantenedor del proyecto, puedes asignar el "issue" a alguien, agregar al proyecto, asociar a un hito, establecer el tipo de incidencia o aplicar una etiqueta. 

7.Enviar: Haz clic en "Submit new issue" (Enviar nueva propuesta). 

Ejemplos de "issues" comunes:
	-Reporte de errores:
"El botón de envío no funciona en la página principal." (Detalles: "Al hacer clic en el botón, no ocurre nada. He probado a actualizar la página y limpiar la caché, pero el problema persiste.").
Petición de nueva funcionalidad:
"Añadir la opción de exportar datos a formato CSV." (Detalles: "Esto facilitaría el análisis de datos fuera de la plataforma.").