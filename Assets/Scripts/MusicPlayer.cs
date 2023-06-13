using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] audioClips; // Массив аудиофайлов для проигрывания
    private AudioSource audioSource; // Компонент AudioSource для проигрывания аудио

    private int currentClipIndex = 0; // Индекс текущего аудиофайла

    private bool isPaused = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource из объекта
        PlayNextClip(); // Запускаем проигрывание первого аудиофайла
        isPaused = false;
    }

    private void PlayNextClip()
    {
        if (currentClipIndex >= audioClips.Length) // Если проигрывание всех аудиофайлов завершено
        {
            return;
        }

        audioSource.clip = audioClips[currentClipIndex]; // Устанавливаем текущий аудиофайл для проигрывания
        audioSource.Play(); // Запускаем проигрывание

        currentClipIndex++; // Переходим к следующему аудиофайлу
    }

    private void Update()
    {
        if (!audioSource.isPlaying) // Если текущий аудиофайл закончился
        {
            PlayNextClip(); // Запускаем проигрывание следующего аудиофайла
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isPaused)
            {
                audioSource.UnPause(); // Снять паузу
                isPaused = false;
            }
            else
            {
                audioSource.Pause(); // Поставить на паузу
                isPaused = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            currentClipIndex++; // Переходим к следующему аудиофайлу
            if (currentClipIndex >= audioClips.Length) // Если проигрывание всех аудиофайлов завершено
            {
                currentClipIndex = 0; // Возвращаемся к первому аудиофайлу
            }
            audioSource.clip = audioClips[currentClipIndex]; // Устанавливаем текущий аудиофайл для проигрывания
            audioSource.Play(); // Запускаем проигрывание
        }
    }
}
