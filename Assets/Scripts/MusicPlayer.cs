using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] audioClips; // Массив аудиофайлов для проигрывания
    private AudioSource audioSource; // Компонент AudioSource для проигрывания аудио

    private int currentClipIndex = 0; // Индекс текущего аудиофайла

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource из объекта
        PlayNextClip(); // Запускаем проигрывание первого аудиофайла
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
    }
}
