# FruitMonster

Crear en Unity un videojuego en 2D llamado FruitMonster. El objetivo del juego es recolectar el
mayor número de frutas posible de entre las que van apareciendo en la pantalla. 

## Boca del monstruo
En el juego debe aparecer un círculo que representa la boca del monstruo de la fruta y que es el
punto que capturará fruta al pulsar la tecla espacio. Este círculo se crea a partir del sprite
mostermouth.png suministrado. La posición inicial de la boca del monstruo debe ser (0, 4, 0)
respecto al centro de la pantalla. Debe girar de forma continua alrededor del centro de la pantalla
en sentido antihorario y a una velocidad de 100 grados/segundo. Para facilitar esto último, lo
montaremos un EmptyObject debidamente configurado.

## Prefabs de fruta
Se deber incorporar al proyecto el sprite fruit.png, que debe ser dividido en 4 frames, de 78 x 78
pixeles. Se crearán cuatro prefab de frutas a partir de cada uno de estos frames, que serán los usados
para el espaneo de las frutas de las que se habla en el siguiente párrafo.

## Espaneo de frutas
Al espanear una fruta esta se colocará en el punto de espaneo, que estará situado en algún lugar de
la circunferencia de radio 4 con centro en el origen. Para ello el punto de espaneo de las frutas se
debe mover mediante un objeto vacío, centrado en (0, 0, 0) que actuará como carrusel. El punto
de espaneo debe estar en el punto (0, 4, 0) relativo a este carrusel. Antes de espanear una fruta
se girará el carrusel un numero aleatorio de grados (siempre un número entero entre 0 y 360), lo que
llevará el punto de espaneo a un lugar aleatorio de la circunferencia de radio 5. En esa localización
se deberá espanear la fruta, manteniendo su orientación original.
Cada vez que se espanee una fruta se deberá escoger aleatoriamente, de forma equiprobable, entre
los cuatro prefabs disponibles.
Se espaneará una fruta al comenzar el juego y, a partir de ahí, cada vez que se capture la fruta de la
pantalla se espaneará otra nueva, como se explica más abajo.

## Captura de frutas
Cuando el jugador presione la tecla espaciadora se calculará la diferencia entre el ángulo de
posición del propio MonsterMouth y el ángulo de posición de la fruta. Si esta diferencia, en valor
absoluto, es menor o igual que 4 grados, se considerará un acierto al capturar la fruta, por lo que la
fruta existente se eliminará, se espaneará otra según lo explicado más arriba, se reproducirá el
sonido win.mp3 y se sumará la puntuación correspondiente a la captura a la cuenta de puntos del
jugador, mostrándose además la nueva puntuación por consola.
Si la diferencia es mayor de 4 grados se considerará un fallo por lo que se mantendrá la fruta en su
lugar, se reproducirá el sonido fail.mp3 y se descontará 1 de las vidas del jugador, mostrándose la
nueva cuenta por consola.

## Puntuación
La puntuación variará según el tipo de fruta capturada. La ciruela valdrá 3 puntos, la manzana 4, el
melocotón 5 y la naranja 6. Además si entre una captura y la siguiente transcurre menos de 0.8
segundos, la puntuación se multiplicará por dos.
La puntuación se mostrará, usando OnGUI(), en la esquina superior derecha.
Al alcanzar la puntuación de 100 se premiará con una vida adicional.

## Aceleración del movimiento.
Por cada 50 puntos alcanzados la velocidad de giro de la boca del monstruo se incrementará en 20
grados/segundo, aumentando así progresivamente la dificultad del juego.

## Cuenta de vidas y Game Over
La cuenta de vidas comenzará en 3 y cuando llegue a 0 se entrará en la condición de Game Over,
por lo que el círculo que representa la boca del monstruo se parará, no respondiéndose además a las
pulsaciones de la tecla espacio.
La cuenta de vidas se mostrará, usando OnGUI(), en la esquina superior izquierda.

RECURSOS SUMINISTRADOS
Nombre del recurso Tipo Uso / se aplica a
mostermouth.png Imagen / Sprite Sprite para la boca del monstruo.
fruits.png Imagen / Sprite Sprite para las frutas.
fail.mp3 Sonido Sonido de fallo al intentar capturar una fruta
win.mp3 Sonido Sonido de éxito al capturar una fruta.
