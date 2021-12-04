SET base_dir=D:\Kabanina\7\SDT\Roulette

cd %base_dir%

SET proc=%base_dir%\_static\preprocess.py


SET prefix=_static\css\deposit

SET src=%prefix%\pr-hist-snap.css
SET targ=%prefix%\pr-hist-snap.out.css
SET var=%prefix%\var.txt

python %proc% -s %src% -t %targ% -v %var%

SET src=%prefix%\deposit.common.css
SET targ=%prefix%\deposit.common.out.css
SET var=%prefix%\var.txt

python %proc% -s %src% -t %targ% -v %var%
