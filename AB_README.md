# Нагрузочное тестирование на GET запросах

## Без балансировки

### 100 одновременно конкурирующих запросов

Команда
```
ab -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6IkFkbWluIiwicm9sZSI6IkFETUlOIiwibmJmIjoxNjY1OTA0OTAxLCJleHAiOjE2NjU5MDg1MDEsImlhdCI6MTY2NTkwNDkwMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDEiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.ZvPJpN2riMM72uSP5OZWw_T_PAeBKb01pxMUIlfLnU0'  -n 100000 -c 100 http://localhost/api/v1/Users
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
Time taken for tests:   174.299 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      61500000 bytes
HTML transferred:       16200000 bytes
Requests per second:    573.73 [#/sec] (mean)
Time per request:       174.299 [ms] (mean)
Time per request:       1.743 [ms] (mean, across all concurrent requests)
Transfer rate:          344.57 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.3      0      11
Processing:     6  174  65.8    161     515
Waiting:        6  168  65.4    154     481
Total:          7  174  65.8    161     516

Percentage of the requests served within a certain time (ms)
  50%    161
  66%    194
  75%    216
  80%    230
  90%    267
  95%    298
  98%    330
  99%    355
 100%    516 (longest request)
```

### 150 одновременно конкурирующих запросов

Команда
```
ab -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6IkFkbWluIiwicm9sZSI6IkFETUlOIiwibmJmIjoxNjY1OTA0OTAxLCJleHAiOjE2NjU5MDg1MDEsImlhdCI6MTY2NTkwNDkwMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDEiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.ZvPJpN2riMM72uSP5OZWw_T_PAeBKb01pxMUIlfLnU0'  -n 100000 -c 150 http://localhost/api/v1/Users
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
Time taken for tests:   191.736 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      61500000 bytes
HTML transferred:       16200000 bytes
Requests per second:    521.55 [#/sec] (mean)
Time per request:       287.603 [ms] (mean)
Time per request:       1.917 [ms] (mean, across all concurrent requests)
Transfer rate:          313.24 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.3      0      14
Processing:     9  287  94.8    276     738
Waiting:        6  280  95.2    269     735
Total:          9  287  94.8    277     738

Percentage of the requests served within a certain time (ms)
  50%    277
  66%    327
  75%    356
  80%    373
  90%    417
  95%    449
  98%    492
  99%    519
 100%    738 (longest request)
```

## С балансировкой

### 100 одновременно конкурирующих запросов

Команда
```
ab -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6IkFkbWluIiwicm9sZSI6IkFETUlOIiwibmJmIjoxNjY1OTA0OTAxLCJleHAiOjE2NjU5MDg1MDEsImlhdCI6MTY2NTkwNDkwMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDEiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.ZvPJpN2riMM72uSP5OZWw_T_PAeBKb01pxMUIlfLnU0'  -n 100000 -c 100 http://localhost/api/v1/Users
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
Time taken for tests:   169.459 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      61500000 bytes
HTML transferred:       16200000 bytes
Requests per second:    590.11 [#/sec] (mean)
Time per request:       169.459 [ms] (mean)
Time per request:       1.695 [ms] (mean, across all concurrent requests)
Transfer rate:          354.41 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.3      0      17
Processing:     6  169  62.3    156     503
Waiting:        5  164  61.8    150     501
Total:          6  169  62.2    156     503

Percentage of the requests served within a certain time (ms)
  50%    156
  66%    187
  75%    208
  80%    222
  90%    257
  95%    288
  98%    321
  99%    341
 100%    503 (longest request)
```

### 150 одновременно конкурирующих запросов

Команда
```
ab -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6IkFkbWluIiwicm9sZSI6IkFETUlOIiwibmJmIjoxNjY1OTA0OTAxLCJleHAiOjE2NjU5MDg1MDEsImlhdCI6MTY2NTkwNDkwMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDEiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.ZvPJpN2riMM72uSP5OZWw_T_PAeBKb01pxMUIlfLnU0'  -n 100000 -c 150 http://localhost/api/v1/Users
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
Time taken for tests:   177.947 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      61500000 bytes
HTML transferred:       16200000 bytes
Requests per second:    561.97 [#/sec] (mean)
Time per request:       266.920 [ms] (mean)
Time per request:       1.779 [ms] (mean, across all concurrent requests)
Transfer rate:          337.51 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.3      0      11
Processing:     6  266  87.8    253     753
Waiting:        6  259  87.6    244     737
Total:          7  267  87.8    253     754

Percentage of the requests served within a certain time (ms)
  50%    253
  66%    301
  80%    344
  90%    388
  95%    422
  98%    460
  99%    496
 100%    754 (longest request)
PS C:\bmstu\sem_7\web> ab -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6IkFkbWluIiwicm9sZSI6IkFETUlOIiwibmJmIjoxNjY1OTA0OTAxLCJleHAiOjE2NjU5MDg1MDEsImlhdCI6MTY2NTkwNDkwMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDEiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.ZvPJpN2riMM72uSP5OZWw_T_PAeBKb01pxMUIlfLnU0'  -n 100000 -c 150 http://localhost/api/v1/Users
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
Time taken for tests:   185.013 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      61500000 bytes
HTML transferred:       16200000 bytes
Requests per second:    540.50 [#/sec] (mean)
Time per request:       277.520 [ms] (mean)
Time per request:       1.850 [ms] (mean, across all concurrent requests)
Transfer rate:          324.62 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.3      0      15
Processing:     7  277  89.8    269     710
Waiting:        7  271  89.7    262     702
Total:          7  277  89.8    269     711

Percentage of the requests served within a certain time (ms)
  50%    269
  66%    313
  75%    340
  80%    356
  90%    399
  95%    435
  98%    472
  99%    503
 100%    711 (longest request)
```

## Вывод
* при увеличении числа конкурирующих запросов результаты ухудшаются;
* с использованием балансировки результаты улучшаются.