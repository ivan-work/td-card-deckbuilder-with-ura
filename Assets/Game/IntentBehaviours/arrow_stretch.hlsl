#ifndef ARROW_STRETCH_INCLUDED
#define ARROW_STRETCH_INCLUDED

float m_stretch(float p, float stretch)
{
  return .5 * (sign(p) * stretch - p) * (sign(abs(p) - stretch) + 1.);
}

void m_stretch_neg_float(float p, float stretch, out float Out)
{
  Out = .5 * m_stretch(2. * p + stretch, stretch);
}

void m_stretch_pos_float(float p, float stretch, out float Out)
{
  Out = .5 * m_stretch(2. * p, stretch);
}


#endif
