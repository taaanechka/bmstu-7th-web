# Нагрузочное тестирование на GET запросах

## Без балансировки

Команда
```
ab -H 'Authorization: Token eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6IkFkbWluIiwicm9sZSI6IkFETUlOIiwibmJmIjoxNjY1ODcyNzExLCJleHAiOjE2NjU4NzYzMTEsImlhdCI6MTY2NTg3MjcxMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDEiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.q3ZLDgv9AniakRLlECfGaBw4FVG4T2f1DJLczes0n7c'  -n 100000 -c 100 http://localhost/api/v1/Users
```

Результат
```
This is ApacheBench, Version 2.3 <$Revision: 1901567 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 10000 requests
Completed 20000 requests
Completed 30000 requests
Completed 40000 requests
Completed 50000 requests
Completed 60000 requests
Completed 70000 requests
Completed 80000 requests
Completed 90000 requests
Completed 100000 requests
Finished 100000 requests


Server Software:        
Server Hostname:        localhost
Server Port:            80       

Document Path:          /api/v1/Users
Document Length:        162 bytes

Concurrency Level:      100
Time taken for tests:   177.859 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      61500000 bytes
HTML transferred:       16200000 bytes
Requests per second:    562.24 [#/sec] (mean)
Time per request:       177.859 [ms] (mean)
Time per request:       1.779 [ms] (mean, across all concurrent requests)
Transfer rate:          337.68 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.3      0      11
Processing:     8  177  68.3    161     518
Waiting:        7  173  68.2    156     503
Total:          8  178  68.3    162     518

Percentage of the requests served within a certain time (ms)
  50%    162
  66%    198
  75%    223
  80%    238
  90%    277
  95%    308
  98%    338
  99%    359
 100%    518 (longest request)
```

Команда
```
ab -H 'Authorization: Token eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6IkFkbWluIiwicm9sZSI6IkFETUlOIiwibmJmIjoxNjY1ODcyNzExLCJleHAiOjE2NjU4NzYzMTEsImlhdCI6MTY2NTg3MjcxMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDEiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.q3ZLDgv9AniakRLlECfGaBw4FVG4T2f1DJLczes0n7c'  -n 100000 -c 150 http://localhost/api/v1/Users
```

Результат
```
This is ApacheBench, Version 2.3 <$Revision: 1901567 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 10000 requests
Completed 20000 requests
Completed 30000 requests
Completed 40000 requests
Completed 50000 requests
Completed 60000 requests
Completed 70000 requests
Completed 80000 requests
Completed 90000 requests
Completed 100000 requests
Finished 100000 requests


Server Software:        
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/Users
Document Length:        162 bytes

Concurrency Level:      150
Time taken for tests:   189.425 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      61500000 bytes
HTML transferred:       16200000 bytes
Requests per second:    527.91 [#/sec] (mean)
Time per request:       284.137 [ms] (mean)
Time per request:       1.894 [ms] (mean, across all concurrent requests)
Transfer rate:          317.06 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.3      0      21
Processing:     7  283  96.8    274     768
Waiting:        5  278  97.3    268     768
Total:          7  284  96.8    274     768

Percentage of the requests served within a certain time (ms)
  50%    274
  66%    326
  75%    354
  80%    371
  90%    415
  95%    448
  98%    491
  99%    523
 100%    768 (longest request)
```

## С балансировкой

Команда
```
ab -H 'Authorization: Token eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6IkFkbWluIiwicm9sZSI6IkFETUlOIiwibmJmIjoxNjY1ODcyNzExLCJleHAiOjE2NjU4NzYzMTEsImlhdCI6MTY2NTg3MjcxMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDEiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.q3ZLDgv9AniakRLlECfGaBw4FVG4T2f1DJLczes0n7c'  -n 100000 -c 100 http://localhost/api/v1/Users
```

Результат
```
This is ApacheBench, Version 2.3 <$Revision: 1901567 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 10000 requests
Completed 20000 requests
Completed 30000 requests
Completed 40000 requests
Completed 50000 requests
Completed 60000 requests
Completed 70000 requests
Completed 80000 requests
Completed 90000 requests
Completed 100000 requests
Finished 100000 requests


Server Software:
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/Users
Document Length:        162 bytes

Concurrency Level:      100
Time taken for tests:   167.726 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      61500000 bytes
HTML transferred:       16200000 bytes
Requests per second:    596.21 [#/sec] (mean)
Time per request:       167.726 [ms] (mean)
Time per request:       1.677 [ms] (mean, across all concurrent requests)
Transfer rate:          358.08 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.3      0      34
Processing:     7  167  61.2    154     461
Waiting:        4  162  61.0    148     461
Total:          7  168  61.2    154     462

Percentage of the requests served within a certain time (ms)
  50%    154
  66%    185
  75%    206
  80%    220
  90%    254
  95%    283
  98%    316
  99%    336
 100%    462 (longest request)
```

Команда
```
ab -H 'Authorization: Token eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6IkFkbWluIiwicm9sZSI6IkFETUlOIiwibmJmIjoxNjY1ODcyNzExLCJleHAiOjE2NjU4NzYzMTEsImlhdCI6MTY2NTg3MjcxMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDEiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.q3ZLDgv9AniakRLlECfGaBw4FVG4T2f1DJLczes0n7c'  -n 100000 -c 150 http://localhost/api/v1/Users
```

Результат
```
This is ApacheBench, Version 2.3 <$Revision: 1901567 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/      

Benchmarking localhost (be patient)
Completed 10000 requests
Completed 20000 requests
Completed 30000 requests
Completed 40000 requests
Completed 50000 requests
Completed 60000 requests
Completed 70000 requests
Completed 80000 requests
Completed 90000 requests
Completed 100000 requests
Finished 100000 requests


Server Software:
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/Users
Document Length:        162 bytes

Concurrency Level:      150
Time taken for tests:   187.275 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      61500000 bytes
HTML transferred:       16200000 bytes
Requests per second:    533.98 [#/sec] (mean)
Time per request:       280.912 [ms] (mean)
Time per request:       1.873 [ms] (mean, across all concurrent requests)
Transfer rate:          320.70 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.3      0      15
Processing:     8  280  95.8    271     740
Waiting:        8  274  95.9    265     739
Total:          8  281  95.8    271     740

Percentage of the requests served within a certain time (ms)
  50%    271
  66%    321
  75%    350
  80%    367
  90%    413
  95%    447
  98%    484
  99%    511
 100%    740 (longest request)
```

## Вывод
* при увеличении числа конкурирующих запросов результаты ухудшаются;
* с использованием балансировки результаты улучшаются.