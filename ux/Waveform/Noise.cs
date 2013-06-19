/* ux - Micro Xylph / Software Synthesizer Core Library
 * Copyright (C) 2013 Tomona Nanase. All rights reserved.
 */

namespace ux.Waveform
{
    /// <summary>
    /// ���`�A�҃V�t�g���W�X�^��p�����������[���m�C�Y�W�F�l���[�^�ł��B
    /// </summary>
	class LongNoise : StepWaveform
	{
		#region Constructors
        /// <summary>
        /// �V���� LongNoise �N���X�̃C���X�^���X�����������܂��B
        /// </summary>
		public LongNoise ()
			: base()
		{
			ushort reg = 0xffff;
			ushort output = 1;

			this.freqFactor = 0.001;
			this.value = new float[32767];
			this.length = 32767;

			for (int i = 0; i < 32767; i++) {
				reg += (ushort)(reg + (((reg >> 14) ^ (reg >> 13)) & 1));
				this.value [i] = (output ^= (ushort)(reg & 1)) * 2.0f - 1.0f;
			}
		}
		#endregion
	}

    /// <summary>
    /// ���`�A�҃V�t�g���W�X�^��p�����Z�����[���m�C�Y�W�F�l���[�^�ł��B
    /// </summary>
    class ShortNoise : StepWaveform
    {
        #region Constructors
        /// <summary>
        /// �V���� ShortNoise �N���X�̃C���X�^���X�����������܂��B
        /// </summary>
        public ShortNoise()
            : base()
        {
            ushort reg = 0xffff;
            ushort output = 1;

            this.freqFactor = 0.001;
            this.value = new float[127];
            this.length = 127;

            for (int i = 0; i < 127; i++)
            {
                reg += (ushort)(reg + (((reg >> 6) ^ (reg >> 5)) & 1));
                this.value[i] = (output ^= (ushort)(reg & 1)) * 2.0f - 1.0f;
            }
        }
        #endregion
    }
}