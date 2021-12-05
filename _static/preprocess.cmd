SET base_dir=D:\Kabanina\7\SDT\Roulette\_static
SET proc=%base_dir%\preprocess.py

CD %base_dir%

CALL _preprocess\pr-global
CALL _preprocess\pr-deposit
CALL _preprocess\pr-deposit.w1920
pause
