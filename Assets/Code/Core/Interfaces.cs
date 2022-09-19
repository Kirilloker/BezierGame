using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public interface IProjectile
{
    public ProjectileEffect GetEffect();
    public float GetEffectValue();
    public void SetEffectValue(float value);
    public void Destroy();
}