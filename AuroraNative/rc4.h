#pragma once

#include "pch.h"

#define N 256   // 2^8

inline void swap(unsigned char* a, unsigned char* b) {
    int tmp = *a;

    *a = *b;
    *b = tmp;
}

inline int ksa(char* key, unsigned char* S) {

    int len = strlen(key);
    int j = 0;

    for (int i = 0; i < N; i++)
        S[i] = i;

    for (int i = 0; i < N; i++) {
        j = (j + S[i] + key[i % len]) % N;

        swap(&S[i], &S[j]);
    }

    return 0;
}

inline int prga(unsigned char* S, char* data, unsigned char* buf) {

    int i = 0;
    int j = 0;

    for (size_t n = 0, len = strlen(data); n < len; n++) {
        i = (i + 1) % N;
        j = (j + S[i]) % N;

        swap(&S[i], &S[j]);

        buf[n] = S[(S[i] + S[j]) % N] ^ data[n];
    }

    return 0;
}

inline int rc4(char* key, char* data, unsigned char* buf) {

    unsigned char S[N];

    ksa(key, S);
    prga(S, data, buf);

    return 0;
}