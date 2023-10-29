# M09UF1-MarcOdisea
Entrega final AA1_Plataformas_3D 

## Contenidos:
### Personaje principal (2 puntos)
Las mecánicas a replicar del movimiento de Mario son:
- [x] Movimiento relativo a la orientación de la cámara con aceleración.
- [x] Salto, doble salto y triple salto.
- [x]  Agacharse y andar agachado.
- [x]  Salto largo y mortal hacía atrás.
- [x]  Salto rebote a pared. --> No he conseguido que el salto sea igual en altura que en horizontal haciendólo en un ángulo de 45 grados (COS y SIN son iguales). Sólo se puede realizar de frente, no de lado.
- [x]  Lanzamiento de Cappy y mecánica de rebote al contactar con él. --> Cappy atraviesa las paredes, no he conseguido que detecte una colisión con una pared y se quede quieta rotando en ese lugar y comience su timer para volver.

### Animaciones (2 puntos)
Se espera que el personaje tenga unas animaciones para sus mecánicas básicas, puede usarse una animación procedural si el alumno lo prefiere para ciertas animaciones.
- [x] Idle.
- [x] Caminar/Correr. --> Hay un instante de colisión y se vuelve loco, he intentado ajustar las ondas de las animaciones pero sigue pasando
- [x] Salto.
- [x] Doble salto.
- [x] Triple salto.

### Colisiones (1.5 puntos)
Por el nivel habrá diversos elementos repartidos que interactúan de la siguiente forma al entrar en contacto con el jugador:
- [x] Elementos estáticos que matan al jugador, al morir se reinicia el nivel. --> Cilindros que te encuentras de cara al iniciar el nivel (Todos)
- [x] Estrellas esparcidas por el mundo, cuando el jugador recolecta X cantidad gana la partida. Cuando una moneda es recogida desaparece del nivel.
- [x] Plataformas de rebote que impulsaran el jugador hacía una dirección. --> Cajas que se encuentran en el agujero del techo y a la izquierda de la entrada. Empujan al jugador en el vector UP de la plataforma.

### Inputs (1.5 puntos)
Se espera que los controles del juego sean multiplataforma y permitan el
control del personaje:
- [x] Usando teclado y ratón.
- [x] Usando un mando conectado al ordenador.

### Cámara (1.5 puntos)
Se espera que la cámara acompañe el gameplay:
- [x] Siguiendo el jugador a una cierta distancia y evitando la pérdida de visión.
- [x] Siendo capaz de orbitar alrededor del personaje controlado a voluntad del jugador.

### Criterio del profesor (1.5 puntos)
Este último apartado quedará a criterio del profesor donde se evaluarán otros elementos como la organización del proyecto, calidad de código, mecánicas adicionales implementadas, sensación de juego, etcétera...
- [x] Vuelta de Cappy. por defecto, a los 10 segundos, Cappy vuelve y se destruye al estar cerca de ti. En ese tiempo no puede vovler
- [x] Animación de Caer al saltar. --> Sólo el salto ya que si no es estable el CharacterController.isGrounded(), no sé si es al aplicar el Move en 2 scripts... :(
- [x] Rotación al realizar WallJump 180 grados.
- [x] Generación de un Audio Mixer con 2 canales, uno para música y otro para efectos de sonido.
- [x] Audio en background que loopea.
- [x] Efecto de sonido al recoger las monedas. Se añade un timer al contactar para que suene el audio y posteriormente se elimine de la escena.

## IGNORE
### Cosas que no son mías 
- Modelos 3D
- Animaciónes del Player
- Audio y música
- Materiales
- Estrellas (modelo y animación)

## CONTROLES
|Control      |Ratón + Teclado|Mando         |
|:-----------:|:-------------:|:------------:|
|Mover        |WASD           |Left Joystick |
|Saltar       |Space          | South Button |
|Agacharse    |Left Ctrl      |Left Shoulder |
|Rotar Cámara |Mouse          |Right Joystick|
|Cappy        |E              |Right Shoulder|
