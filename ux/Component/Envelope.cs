/* ux - Micro Xylph / Software Synthesizer Core Library
 * Copyright (C) 2013 Tomona Nanase. All rights reserved.
 */
using ux.Utils;

namespace ux.Component
{
    /// <summary>
    /// ���Ԃɂ���ĕω�����p�����[�^���������邽�߂̃G���x���[�v (���) �N���X�ł��B
    /// </summary>
	class Envelope
	{
		#region Private Members
		private readonly float samplingFreq;
		private int releaseStartTime, t2, t3, t5;
		private float da, dd, dr;
		#endregion

		#region Public Proparties
        /// <summary>
        /// ���݂̃G���x���[�v�̏�Ԃ�\���񋓒l���擾���܂��B
        /// </summary>
		public EnvelopeState State { get; private set; }

        /// <summary>
        /// �A�^�b�N���Ԃ��擾���܂��B
        /// </summary>
		public int AttackTime { get; private set; }

        /// <summary>
        /// �s�[�N���Ԃ��擾���܂��B
        /// </summary>
		public int PeakTime { get; private set; }

        /// <summary>
        /// �f�B�P�C���Ԃ��擾���܂��B
        /// </summary>
		public int DecayTime { get; private set; }

        /// <summary>
        /// �T�X�e�B�����x�����擾���܂��B
        /// </summary>
		public float SustainLevel { get; private set; }

        /// <summary>
        /// �����[�X���Ԃ��擾���܂��B
        /// </summary>
		public int ReleaseTime { get; private set; }
		#endregion

		#region Constructor
        /// <summary>
        /// �T���v�����O���g�����w�肵�ĐV���� Envelope �N���X�̃C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="samplingFreq">�T���v�����O���g���B</param>
		public Envelope(float samplingFreq)
		{
			this.samplingFreq = samplingFreq;
			this.Reset();
		}
		#endregion

		#region Public Methods
        /// <summary>
        /// ���̃C���X�^���X�ɂ����邷�ׂẴp�����[�^������l�ɖ߂��܂��B
        /// </summary>
		public void Reset()
		{
			this.AttackTime = (int)(0.05f * this.samplingFreq);
			this.PeakTime = (int)(0.0f * this.samplingFreq);
			this.DecayTime = (int)(0.0f * this.samplingFreq);
			this.SustainLevel = 1.0f;
			this.ReleaseTime = (int)(0.05f * this.samplingFreq);
			this.State = EnvelopeState.Silence;
		}

        /// <summary>
        /// �G���x���[�v�̏�Ԃ��A�^�b�N��ԂɕύX���܂��B
        /// </summary>
		public void Attack()
		{
			this.State = EnvelopeState.Attack;

			//precalc
			this.t2 = this.AttackTime + this.PeakTime;
			this.t3 = t2 + this.DecayTime;
			this.da = 1.0f / this.AttackTime;
			this.dd = (1.0f - this.SustainLevel) / this.DecayTime;
		}

        /// <summary>
        /// �G���x���[�v�̏�Ԃ������[�X��ԂɕύX���܂��B
        /// </summary>
        /// <param name="time">�����[�X���J�n���ꂽ���Ԓl�B</param>
		public void Release(int time)
		{
			if (this.State == EnvelopeState.Attack)
			{
				this.State = EnvelopeState.Release;
				this.releaseStartTime = time;

				//precalc
				this.t5 = time + this.ReleaseTime;
				this.dr = this.SustainLevel / this.ReleaseTime;
			}
		}

        /// <summary>
        /// �G���x���[�v�̏�Ԃ��T�C�����X��ԂɕύX���܂��B
        /// </summary>
		public void Silence()
		{
			this.State = EnvelopeState.Silence;
		}

        /// <summary>
        /// ���݂̃G���x���[�v�̏�ԂɊ�Â��A�G���x���[�v�l���o�͂��܂��B
        /// </summary>
        /// <param name="time">�G���x���[�v�̊J�n���Ԓl�B</param>
        /// <param name="envelopes">�o�͂��i�[���������̔z��B</param>
        /// <param name="offset">������J�n�����z��̃C���f�b�N�X�B</param>
        /// <param name="count">������������l�̐��B</param>
		public void Generate(int time, float[] envelopes, int offset, int count)
		{
			float res;
			for (int i = offset; i < count; i++, time++)
			{
				if (this.State == EnvelopeState.Attack)
					res = (time < this.AttackTime) ? time * this.da :
					  (time < this.t2) ? 1.0f :
					  (time < this.t3) ? 1.0f - (time - this.t2) * this.dd : this.SustainLevel;
				else if (this.State == EnvelopeState.Release)
					if (time < this.t5)
						res = this.SustainLevel - (time - this.releaseStartTime) * this.dr;
					else
					{
						res = 0.0f;
						this.State = EnvelopeState.Silence;
					}
				else
					res = 0.0f;

				envelopes[i] = res;
			}
		}

        /// <summary>
        /// �p�����[�^��p���Ă��̃G���x���[�v�̐ݒ�l��ύX���܂��B
        /// </summary>
        /// <param name="parameter">�p�����[�^��\�� PValue�B</param>
		public void SetParameter(PValue parameter)
		{
			switch (parameter.Name)
			{
				case "a":
				case "attack":
					this.AttackTime = (int)(parameter.Value.Clamp(float.MaxValue, 0.0f) * this.samplingFreq);
					break;

				case "p":
				case "peak":
					this.PeakTime = (int)(parameter.Value.Clamp(float.MaxValue, 0.0f) * this.samplingFreq);
					break;

				case "d":
				case "decay":
					this.DecayTime = (int)(parameter.Value.Clamp(float.MaxValue, 0.0f) * this.samplingFreq);
					break;

				case "s":
				case "sustain":
					this.SustainLevel = parameter.Value.Clamp(1.0f, 0.0f);
					break;

				case "r":
				case "release":
					this.ReleaseTime = (int)(parameter.Value.Clamp(float.MaxValue, 0.0f) * this.samplingFreq);
					break;

				default:
					break;
			}
		}
		#endregion
	}
}
