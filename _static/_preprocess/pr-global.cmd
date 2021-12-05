SET prefix=css\

SET src=%prefix%\global.css
SET targ=%prefix%\global.out.css
SET var=%prefix%\var.txt

python %proc% -s %src% -t %targ% -v %var%
