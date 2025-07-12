using System.Text;
using Sylais.Models;

namespace Sylais.Commands;

public class BaseCommand
{
    protected StringBuilder _outBuffer = new StringBuilder();
    protected StringBuilder _errBuffer = new StringBuilder();
}
