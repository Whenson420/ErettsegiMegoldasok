SELECT alloviz.nev, terulet, telepulesgps.nev
FROM alloviz, helykapcs, telepulesgps
WHERE alloviz.id=allovizid And telepulesgps.id=gpsid And
	  allovizid ... And
	  gpsid ...;
