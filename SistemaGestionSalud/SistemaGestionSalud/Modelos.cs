using System;

namespace SistemaGestionSalud {

    /// <summary>
    /// Estado de un turno dentro de la cola de una unidad de salud.
    /// </summary>
    public enum EstadoTurno {
        EnEspera,
        Atendido
    }

    /// <summary>
    /// Nivel de gravedad (triage) de un paciente. El valor numérico define
    /// la prioridad de atención: entre menor el número, antes se atiende.
    /// </summary>
    public enum NivelGravedad {
        Critico = 1,
        Urgente = 2,
        Leve = 3
    }

    /// <summary>
    /// Estado de una cita agendada.
    /// </summary>
    public enum EstadoCita {
        Programada,
        Cancelada,
        Atendida
    }

    /// <summary>
    /// Representa un turno de un paciente en la cola de espera de una unidad de salud.
    /// Todo se maneja en memoria (no hay base de datos).
    /// </summary>
    public class Turno {
        public int Numero { get; set; }
        public string Paciente { get; set; }
        public string Unidad { get; set; }
        public NivelGravedad Gravedad { get; set; }
        public DateTime HoraLlegada { get; set; }
        public DateTime? HoraAtencion { get; set; }
        public EstadoTurno Estado { get; set; }

        /// <summary>
        /// Tiempo que el paciente ha esperado (o esperó) desde que tomó el turno.
        /// </summary>
        public TimeSpan TiempoEspera {
            get {
                DateTime referencia = HoraAtencion ?? DateTime.Now;
                return referencia - HoraLlegada;
            }
        }

        /// <summary>
        /// Texto en español (con tilde) para mostrar el nivel de gravedad.
        /// </summary>
        public string GravedadTexto {
            get {
                switch (Gravedad) {
                    case NivelGravedad.Critico: return "Crítico";
                    case NivelGravedad.Urgente: return "Urgente";
                    case NivelGravedad.Leve: return "Leve";
                    default: return Gravedad.ToString();
                }
            }
        }
    }

    /// <summary>
    /// Representa una cita agendada por un paciente en una unidad de salud.
    /// </summary>
    public class Cita {
        public string Paciente { get; set; }
        public string Unidad { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Motivo { get; set; }
        public EstadoCita Estado { get; set; }

        public DateTime FechaHora {
            get { return Fecha.Date + Hora.TimeOfDay; }
        }
    }
}
